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
using SuperCarga.Application.Domain.Finances.Model;
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

        public CustomerContractsService(
            SuperCargaContext ctx, 
            ICostsService costsService, 
            IDriversService driversService) : base(ctx)
        {
            this.costsService = costsService;
            this.driversService = driversService;
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

            await UnholdAndTransfer(contract, false);

            await ctx.SaveChangesAsync();
        }

        private async Task UnholdAndTransfer(Contract contract, bool save)
        {
            var customerFinance = await ctx.Finances
                .Include(x => x.Holds)
                .Where(x => x.UserId == contract.Customer.User.Id)
                .FirstOrDefaultAsync();

            var hold = customerFinance.Holds
                .Where(x => x.RelatedContractId == contract.Id)
                .FirstOrDefault();

            ctx.BalanceHolds.Remove(hold);

            var customerHistory = new FinanceHistory
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                FinanceId = customerFinance.Id,
                Operation = FinanceOperation.Transfer,
                BalanceBefore = customerFinance.Balance,
                BalanceAfter = customerFinance.Balance,
                OperationValue = hold.Value,
                RelatedContractId = contract.Id,
                FromUserId = contract.Customer.User.Id,
                ToUserId = contract.Driver.User.Id,
            };
            ctx.FinancesHistory.Add(customerHistory);

            var driverFinance = await ctx.Finances
                .Where(x => x.UserId == contract.Driver.User.Id)
                .FirstOrDefaultAsync();

            var newDriverBalance = driverFinance.Balance + hold.Value;

            var driverHistory = new FinanceHistory
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                FinanceId = driverFinance.Id,
                Operation = FinanceOperation.Transfer,
                BalanceBefore = driverFinance.Balance,
                BalanceAfter = newDriverBalance,
                OperationValue = hold.Value,
                RelatedContractId = contract.Id,
                FromUserId = contract.Customer.User.Id,
                ToUserId = contract.Driver.User.Id,
            };
            ctx.FinancesHistory.Add(driverHistory);

            driverFinance.Balance = newDriverBalance;

            contract.PaymentState = ContractPaymentState.PrepaymentReceived;

            if(save)
            {
                await ctx.SaveChangesAsync();
            }
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

            await Transfer(contract, request.Data.Payment, false);

            await ctx.SaveChangesAsync();
        }

        private async Task Transfer(Contract contract, decimal paymentValue, bool save)
        {
            var customerFinance = await ctx.Finances
                .Where(x => x.UserId == contract.Customer.User.Id)
                .FirstOrDefaultAsync();

            var newCustomerBalance = customerFinance.Balance - paymentValue;

            var customerHistory = new FinanceHistory
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                FinanceId = customerFinance.Id,
                Operation = FinanceOperation.Transfer,
                BalanceBefore = customerFinance.Balance,
                BalanceAfter = newCustomerBalance,
                OperationValue = paymentValue,
                RelatedContractId = contract.Id,
                FromUserId = contract.Customer.User.Id,
                ToUserId = contract.Driver.User.Id,
            };
            ctx.FinancesHistory.Add(customerHistory);

            customerFinance.Balance = newCustomerBalance;

            var driverFinance = await ctx.Finances
                .Where(x => x.UserId == contract.Driver.User.Id)
                .FirstOrDefaultAsync();

            var newDriverBalance = driverFinance.Balance + paymentValue;

            var driverHistory = new FinanceHistory
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                FinanceId = driverFinance.Id,
                Operation = FinanceOperation.Transfer,
                BalanceBefore = driverFinance.Balance,
                BalanceAfter = newDriverBalance,
                OperationValue = paymentValue,
                RelatedContractId = contract.Id,
                FromUserId = contract.Customer.User.Id,
                ToUserId = contract.Driver.User.Id,
            };
            ctx.FinancesHistory.Add(driverHistory);

            driverFinance.Balance = newDriverBalance;

            contract.PaymentState = ContractPaymentState.PaidFull;

            if (save)
            {
                await ctx.SaveChangesAsync();
            }
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

            //TODO transfer fee to SC account
            await AddBalanceHold(request.User.Id, request.Data.Payment, contract.Id, false);

            await ctx.Contracts.AddAsync(contract);
            await ctx.ContractAdditionalCosts.AddRangeAsync(additions);
            await ctx.ContractHistories.AddAsync(history);
            
            await ctx.SaveChangesAsync();

            return contract.Id;
        }

        private async Task UpdateDriversContracts(Guid driverId)
        {
            var driver = await ctx.Drivers
                .Where(x => x.Id == driverId)
                .FirstOrDefaultAsync();

            driver.Contracts++;
        }

        private async Task AddBalanceHold(Guid userId, decimal holdValue, Guid contractId, bool save)
        {
            var customerFinances = await ctx.Finances
                .Where(x => x.UserId == userId)
                .FirstOrDefaultAsync();

            if (customerFinances.Balance < holdValue)
            {
                throw new ValidationException("Payment value can not be bigger than user Balance.");
            }

            var newBalance = customerFinances.Balance - holdValue;

            var financeHistory = new FinanceHistory
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                FinanceId = customerFinances.Id,
                Operation = FinanceOperation.Hold,
                BalanceBefore = customerFinances.Balance,
                BalanceAfter = newBalance,
                OperationValue = holdValue,
                RelatedContractId = contractId
            };

            var balanceHold = new BalanceHold
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                FinanceId = customerFinances.Id,
                Value = holdValue,
                RelatedContractId = contractId
            };

            customerFinances.Balance = newBalance;

            await ctx.FinancesHistory.AddAsync(financeHistory);
            await ctx.BalanceHolds.AddAsync(balanceHold);

            if (save)
            {
                await ctx.SaveChangesAsync();
            }
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
                    CostsSummary = x.GetCostsSummary()
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
