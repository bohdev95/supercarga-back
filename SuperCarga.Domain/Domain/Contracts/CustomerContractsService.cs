using Microsoft.EntityFrameworkCore;
using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Contracts.Common.Dto;
using SuperCarga.Application.Domain.Contracts.Common.Models;
using SuperCarga.Application.Domain.Contracts.Customers.Abstraction;
using SuperCarga.Application.Domain.Contracts.Customers.Commands.Add;
using SuperCarga.Application.Domain.Contracts.Customers.Commands.ConfirmDelivery;
using SuperCarga.Application.Domain.Contracts.Customers.Commands.Finalize;
using SuperCarga.Application.Domain.Contracts.Customers.Queries.Details;
using SuperCarga.Application.Domain.Contracts.Customers.Queries.Details.Dto;
using SuperCarga.Application.Domain.Contracts.Customers.Queries.List.Active;
using SuperCarga.Application.Domain.Contracts.Customers.Queries.List.Finished;
using SuperCarga.Application.Domain.Costs.Abstraction;
using SuperCarga.Application.Domain.Costs.Dto;
using SuperCarga.Application.Domain.Drivers.Common.Abstraction;
using SuperCarga.Application.Domain.Drivers.Customers.Dto;
using SuperCarga.Application.Domain.Finances.Abstraction;
using SuperCarga.Application.Domain.Finances.Model;
using SuperCarga.Application.Domain.Location.Dto;
using SuperCarga.Application.Domain.Proposals.Common.Models;
using SuperCarga.Application.Domain.Users.Models;
using SuperCarga.Application.Exceptions;
using SuperCarga.Application.Validation;
using SuperCarga.Persistence.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Domain.Domain.Contracts
{
    public class CustomerContractsService : ContractsService, ICustomerContractsService
    {
        private readonly ICostsService costsService;
        private readonly IDriversService driversService;
        private readonly IFinancesService financesService;

        public CustomerContractsService(
            SuperCargaContext ctx, 
            ICostsService costsService, 
            IDriversService driversService,
            IFinancesService financesService) : base(ctx)
        {
            this.costsService = costsService;
            this.driversService = driversService;
            this.financesService = financesService;
        }

        public async Task ConfirmDelivery(CustomerConfirmDeliveryCommand request)
        {
            var contract = await ctx.Contracts
                .Include(x => x.Driver)
                .ThenInclude(x => x.User)
                .Include(x => x.Customer)
                .ThenInclude(x => x.User)
                .Where(x => x.Id == request.Data.ContractId)
                .FirstOrDefaultAsync();

            if (contract.CustomerId != request.User.CustomerId.Value)
            {
                throw new ForbiddenException();
            }

            await UpdateContractStatus(contract, ContractState.DeliveredConfirmed, false);

            financesService.PaymentLock(() =>
            { 
                var holdValue = financesService.RemoveHold(contract.Customer.User.Id, contract.Id, false);
                var driverFirstPayment = holdValue - contract.ServiceFee;
                financesService.PayFee(contract.Customer.User.Id, contract.Id, false);
                financesService.Pay(contract.Customer.User.Id, contract.Driver.User.Id, driverFirstPayment, FinanceOperation.Transfer, contract.Id, false);
                ctx.SaveChangesAsync();
            });
        }

        public async Task Finalize(CustomerFinalizeCommand request)
        {
            var contract = await ctx.Contracts
                .Include(x => x.Driver)
                .ThenInclude(x => x.User)
                .Include(x => x.Customer)
                .ThenInclude(x => x.User)
                .Include(x => x.Proposal)
                .Where(x => x.Id == request.Data.ContractId)
                .FirstOrDefaultAsync();

            if (contract.CustomerId != request.User.CustomerId.Value)
            {
                throw new ForbiddenException();
            }

            await UpdateContractStatus(contract, ContractState.Closed, false);

            contract.Rating = request.Data.Rating;
            contract.RatingComment = request.Data.RatingComment;
            contract.Proposal.State = ProposalState.Closed;

            await driversService.UpdateDriverRates(contract.DriverId, false);

            financesService.PaymentLock(() =>
            {
                financesService.Pay(contract.Customer.User.Id, contract.Driver.User.Id, request.Data.Payment, FinanceOperation.Transfer, contract.Id, false);
                ctx.SaveChangesAsync();
            });
        }

        public async Task<Guid> AddContract(AddContractCommand request)
        {
            var proposal = await ctx.Proposals
                .Include(x => x.Job)
                .Where(x => x.Id == request.Data.ProposalId)
                .FirstOrDefaultAsync();

            if (proposal.Job.CustomerId != request.User.CustomerId)
            {
                throw new ForbiddenException();
            }

            var contractAlreadyExists = await ctx.Contracts
                .Where(x => x.ProposalId == request.Data.ProposalId)
                .AnyAsync();

            if (contractAlreadyExists)
            {
                throw new AlreadyExistsException(ValidationMessage.AlreadyExist("Contract"));
            }

            var costs = await costsService.CalculateCostsSummary(new CaluclateCostsSummaryDto
            {
                PricePerKm = proposal.PricePerKm,
                Distance = proposal.Job.Distance,
                RequireLoadingCrew = proposal.Job.RequireLoadingCrew,
                RequireUnloadingCrew = proposal.Job.RequireUnloadingCrew
            });

            var contract = new Contract
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                ProposalId = proposal.Id,
                JobId = proposal.JobId,
                DriverId = proposal.DriverId,
                CustomerId = proposal.Job.CustomerId,
                State = ContractState.Created,
                PaymentState = ContractPaymentState.OnDeliveryConfirmation,
                PricePerKm = costs.PricePerKm,
                PricePerDistance = costs.PricePerDistance,
                TotalPrice = costs.TotalPrice,
                ServiceFee = costs.ServiceFee,
                Price = costs.Price
            };

            var additions = costs.Additions.Select(x => new ContractAdditionalCost
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                ContractId = contract.Id,
                Name = x.Name,
                Price = x.Price
            })
            .ToList();

            var history = new ContractHistory
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                ContractId = contract.Id,
                State = contract.State
            };

            proposal.State = ProposalState.Accepted;

            await UpdateDriversContracts(proposal.DriverId);
            await ctx.Contracts.AddAsync(contract);
            await ctx.ContractAdditionalCosts.AddRangeAsync(additions);
            await ctx.ContractHistories.AddAsync(history);

            financesService.PaymentLock(() =>
            {
                financesService.AddHold(request.User.Id, request.Data.Payment, contract.Id, false);
                ctx.SaveChangesAsync();
            });

            return contract.Id;
        }

        private async Task UpdateDriversContracts(Guid driverId)
        {
            var driver = await ctx.Drivers
                .Where(x => x.Id == driverId)
                .FirstOrDefaultAsync();

            driver.Contracts++;
        }

        public async Task<CustomerContractDetailsDto> GetContractDetails(GetCustomerContractDetailsQuery request)
        {
            var dto = await ctx.Contracts
                .Include(x => x.Payments)
                .Include(x => x.Additions)
                .Include(x => x.Job)
                .ThenInclude(x => x.VehiculeType)
                .Include(x => x.Driver)
                .ThenInclude(x => x.User)
                .Where(x => x.Id == request.Data.ContractId)
                .Where(x => x.CustomerId == request.User.CustomerId.Value)
                .Select(x => new CustomerContractDetailsDto
                {
                    Id = x.Id,
                    Created = x.Created,
                    Rating = x.Rating,
                    VehiculeTypeName = x.Job.VehiculeType.Name,
                    Description = x.Job.Description,
                    Tittle = x.Job.Tittle,
                    Distance = x.Job.Distance,
                    PickupDate = x.Job.PickupDate,
                    DeliveryDate = x.Job.DeliveryDate,
                    Cargo = x.Job.GetDimensionsCargo(),
                    Origin = x.Job.GetOrigin(),
                    Destination = x.Job.GetDestination(),
                    PaymentState = x.PaymentState,
                    State = x.State,
                    StateChanged = x.History.OrderByDescending(x => x.Created).Select(x => x.Created).FirstOrDefault(),
                    IsInDispute = false, //TODO
                    PickUpCargoImagePath = x.PickUpCargoImagePath,
                    DeliveryCargoImagePath = x.DeliveryCargoImagePath,
                    PickUpProofImagePath = x.PickUpProofImagePath,
                    DeliveryProofImagePath = x.DeliveryProofImagePath,
                    Driver = x.Driver.GetCustomerDriverDto(),
                    CostsSummary = x.GetCostsSummary(),
                    Payments = x.GetContractPayment()
                })
                .FirstOrDefaultAsync();

            return dto;
        }

        public async Task<ListResponseDto<ActiveContractListITemDto>> ListActiveContracts(CustomerListActiveContractsQuery request) =>
            await ListActiveContracts(request.Data, x => x.CustomerId == request.User.CustomerId.Value);

        public async Task<ListResponseDto<FinishedContractListITemDto>> ListFinishedContracts(CustomerListFinishedContractsQuery request) =>
            await ListFinishedContracts(request.Data, x => x.CustomerId == request.User.CustomerId.Value);

    }
}
