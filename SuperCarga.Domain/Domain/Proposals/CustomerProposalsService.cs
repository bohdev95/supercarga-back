using Microsoft.EntityFrameworkCore;
using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Contracts.Common.Models;
using SuperCarga.Application.Domain.Costs.Abstraction;
using SuperCarga.Application.Domain.Costs.Dto;
using SuperCarga.Application.Domain.Drivers.Customers.Dto;
using SuperCarga.Application.Domain.Location.Dto;
using SuperCarga.Application.Domain.Proposals.Customers.Abstraction;
using SuperCarga.Application.Domain.Proposals.Customers.Commands.AddToFavorites;
using SuperCarga.Application.Domain.Proposals.Customers.Commands.Hire;
using SuperCarga.Application.Domain.Proposals.Customers.Commands.RemoveFromFavorites;
using SuperCarga.Application.Domain.Proposals.Customers.Queries.Details;
using SuperCarga.Application.Domain.Proposals.Customers.Queries.Details.Dto;
using SuperCarga.Application.Domain.Proposals.Customers.Queries.List.All;
using SuperCarga.Application.Domain.Proposals.Customers.Queries.List.Common.Dto;
using SuperCarga.Application.Domain.Proposals.Customers.Queries.List.Favorites;
using SuperCarga.Application.Exceptions;
using SuperCarga.Application.Validation;
using SuperCarga.Domain.Domain.Common;
using SuperCarga.Persistence.Database;

namespace SuperCarga.Domain.Domain.Proposals
{
    public class CustomerProposalsService : ICustomerProposalsService
    {
        private readonly SuperCargaContext ctx;
        private readonly ICostsService costsService;

        public CustomerProposalsService(SuperCargaContext ctx, ICostsService costsService)
        {
            this.ctx = ctx;
            this.costsService = costsService;
        }

        public async Task<ListResponseDto<CustomerProposalListItemDto>> ListAllProposals(CustomerListAllProposalsQuery request)
        {
            var query = await ListProposals(request.User.CustomerId.Value, request.Data.JobId);

            var res = await query
                .Paginate(request.Data);

            return res;
        }

        public async Task<ListResponseDto<CustomerProposalListItemDto>> ListFavoritesProposals(CustomerListFavoritesProposalsQuery request)
        {
            var query = await ListProposals(request.User.CustomerId.Value, request.Data.JobId);

            var res = await query
                .Where(x => x.AddedToFavorite)
                .Paginate(request.Data);

            return res;
        }

        private async Task<IQueryable<CustomerProposalListItemDto>> ListProposals(Guid customerId, Guid? jobId)
        {
            var query = ctx.Proposals
                .Include(x => x.Driver)
                .ThenInclude(x => x.User)
                .Include(x => x.Job)
                .ThenInclude(x => x.Contracts)
                .Where(x => x.Job.CustomerId == customerId)
                .Where(x => !x.Job.Contracts.Where(y => y.ProposalId == x.Id).Any())
                .AsQueryable();

            if(jobId != null)
            {
                query = query
                    .Where(x => x.JobId == jobId)
                    .AsQueryable();
            }

            var resQuery = query.Select(x => new CustomerProposalListItemDto
            {
                Id = x.Id,
                Created = x.Created,
                PricePerKm = x.PricePerKm,
                JobId = x.JobId,
                AddedToFavorite = x.AddedToFavoriteBy.Where(x => x.Id == customerId).Any(),
                Driver = x.Driver.GetCustomerDriverDto(),
                State = x.State
            })
            .OrderByDescending(x => x.Created)
            .AsQueryable();

            return resQuery;
        }

        public async Task<Guid> Hire(HireCommand request)
        {
            var proposal = await ctx.Proposals
                .Include(x => x.Job)
                .Where(x => x.Id == request.Data.ProposalId)
                .FirstOrDefaultAsync();

            if (proposal.Job.CustomerId != request.User.CustomerId)
                throw new ForbiddenException();

            var contractAlreadyExists = await ctx.Contracts
                .Where(x => x.ProposalId == request.Data.ProposalId)
                .AnyAsync();

            if (contractAlreadyExists)
                throw new AlreadyExistsException(ValidationMessage.AlreadyExist("Contract"));

            var contract = new Contract
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                ProposalId = proposal.Id,
                JobId = proposal.JobId,
                DriverId = proposal.DriverId,
                CustomerId = proposal.Job.CustomerId,
                State = ContractState.Started
            };

            var contractHistory = new ContractHistory
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                ContractId = contract.Id,
                State = contract.State
            };

            await ctx.Contracts.AddAsync(contract);
            await ctx.ContractHistories.AddAsync(contractHistory);
            await ctx.SaveChangesAsync();

            return contract.Id;
        }

        public async Task AddProposalToFavorites(AddProposalToFavoritesCommand request)
        {
            var customer = await ctx.Customers
                .Include(x => x.FavoriteProposals)
                .Where(x => x.Id == request.User.CustomerId.Value)
                .FirstOrDefaultAsync();

            var alreadyAdded = customer.FavoriteProposals
                .Where(x => x.Id == request.Data.ProposalId).Any();

            if (!alreadyAdded)
            {
                var proposal = await ctx.Proposals
                    .Where(x => x.Id == request.Data.ProposalId)
                    .FirstOrDefaultAsync();

                customer.FavoriteProposals.Add(proposal);

                await ctx.SaveChangesAsync();
            }
        }

        public async Task RemoveProposalFromFavorites(RemoveProposalFromFavoritesCommand request)
        {
            var customer = await ctx.Customers
                .Include(x => x.FavoriteProposals)
                .Where(x => x.Id == request.User.CustomerId.Value)
                .FirstOrDefaultAsync();

            customer.FavoriteProposals.RemoveAll(x => x.Id == request.Data.ProposalId);

            await ctx.SaveChangesAsync();
        }

        //TODO same code as driver
        public async Task<CustomerProposalDetailsDto> GetProposalsDetails(GetCustomerProposalDetailsQuery request)
        {
            var proposalsJobWasCreatedByCustomer = await ctx.Proposals
                .Where(x => x.Id == request.Data.ProposalId)
                .Include(x => x.Job)
                .Where(x => x.Job.CustomerId == request.User.CustomerId.Value)
                .AnyAsync();

            if (!proposalsJobWasCreatedByCustomer)
            {
                throw new ForbiddenException();
            }

            await SetChecked(request.Data.ProposalId);

            var dto = await ctx.Proposals
                .Include(x => x.Job)
                .ThenInclude(x => x.VehiculeType)
                .Include(x => x.Driver)
                .ThenInclude(x => x.User)
                .Where(x => x.Id == request.Data.ProposalId)
                .Select(x => new CustomerProposalDetailsDto
                {
                    Id = x.Id,
                    ProposalCreated = x.Created,
                    JobCreated = x.Created,
                    VehiculeTypeName = x.Job.VehiculeType.Name,
                    RequireLoadingCrew = x.Job.RequireLoadingCrew,
                    RequireUnloadingCrew = x.Job.RequireUnloadingCrew,
                    Description = x.Job.Description,
                    Tittle = x.Job.Tittle,
                    Distance = x.Job.Distance,
                    JobsPricePerKm = x.Job.PricePerKm,
                    DriversPricePerKm = x.PricePerKm,
                    State = x.State,
                    PickupDate = x.Job.PickupDate,
                    DeliveryDate = x.Job.DeliveryDate,
                    Cargo = x.Job.GetDimensionsCargo(),
                    Origin = x.Job.GetOrigin(),
                    Destination = x.Job.GetDestination(),
                    Driver = x.Driver.GetCustomerDriverDto()
                })
                .FirstOrDefaultAsync();

            var contract = await ctx.Contracts
                .Include(x => x.Additions)
                .Where(x => x.ProposalId == request.Data.ProposalId)
                .FirstOrDefaultAsync();

            if (contract != null)
            {
                dto.CostsSummary = contract.GetCostsSummary();
            }
            else
            {
                dto.CostsSummary = await costsService.CalculateCostsSummary(new CaluclateCostsSummaryDto
                {
                    PricePerKm = dto.DriversPricePerKm,
                    Distance = dto.Distance,
                    RequireLoadingCrew = dto.RequireLoadingCrew,
                    RequireUnloadingCrew = dto.RequireUnloadingCrew
                });
            }

            return dto;
        }

        private async Task SetChecked(Guid proposalId)
        {
            var proposal = await ctx.Proposals
                .Where(x => x.Id == proposalId)
                .FirstOrDefaultAsync();

            if(!proposal.Checked)
            {
                proposal.Checked = true;
                await ctx.SaveChangesAsync();
            }
        }

    }
}
