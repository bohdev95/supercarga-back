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
using SuperCarga.Application.Domain.Location.Dto;
using SuperCarga.Application.Domain.Proposals.Common.Models;
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

        public CustomerContractsService(SuperCargaContext ctx, ICostsService costsService, IDriversService driversService) : base(ctx)
        {
            this.costsService = costsService;
            this.driversService = driversService;
        }

        public async Task ConfirmDelivery(CustomerConfirmDeliveryCommand request)
        {
            await UpdateContractStatus(async contract =>
            {
                contract.State = ContractState.DeliveredConfirmed;
            },
            request.Data.ContractId,
            request.User.CustomerId.Value);
        }

        public async Task Finalize(CustomerFinalizeCommand request)
        {
            await UpdateContractStatus(async contract =>
            {
                contract.State = ContractState.Closed;
                contract.Rating = request.Data.Rating;
                contract.RatingComment = request.Data.RatingComment;
            },
            request.Data.ContractId,
            request.User.CustomerId.Value);

            var driverId = await ctx.Contracts
                .Where(x => x.Id == request.Data.ContractId)
                .Select(x => x.DriverId)
                .FirstOrDefaultAsync();

            await driversService.UpdateDriverRates(driverId);
        }

        private async Task UpdateContractStatus(Func<Contract, Task> update, Guid contractId, Guid customerId)
        {
            var contract = await ctx.Contracts
                .Where(x => x.Id == contractId)
                .FirstOrDefaultAsync();

            if (contract.CustomerId != customerId)
            {
                throw new ForbiddenException();
            }

            await UpdateContractStatus(update, contract);
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

            var driver = await ctx.Drivers
                .Where(x => x.Id == proposal.DriverId)
                .FirstOrDefaultAsync();

            driver.Contracts++;

            await ctx.Contracts.AddAsync(contract);
            await ctx.ContractAdditionalCosts.AddRangeAsync(additions);
            await ctx.ContractHistories.AddAsync(history);
            await ctx.SaveChangesAsync();

            return contract.Id;
        }

        public async Task<CustomerContractDetailsDto> GetContractDetails(GetCustomerContractDetailsQuery request)
        {
            var dto = await ctx.Contracts
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
                    PricePerKm = x.PricePerKm,
                    PickupDate = x.Job.PickupDate,
                    DeliveryDate = x.Job.DeliveryDate,
                    Cargo = x.Job.GetDimensionsCargo(),
                    Origin = x.Job.GetOrigin(),
                    Destination = x.Job.GetDestination(),
                    Driver = x.Driver.GetCustomerDriverDto(),
                    PaymentState = "On delivery confirmation", //TODO
                    State = x.State,
                    StateChanged = x.History.OrderByDescending(x => x.Created).Select(x => x.Created).FirstOrDefault(),
                    IsInDispute = false, //TODO
                    Price = x.Price,
                    PickUpCargoImagePath = x.PickUpCargoImagePath,
                    DeliveryCargoImagePath = x.DeliveryCargoImagePath,
                    PickUpProofImagePath = x.PickUpProofImagePath,
                    DeliveryProofImagePath = x.DeliveryProofImagePath
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
