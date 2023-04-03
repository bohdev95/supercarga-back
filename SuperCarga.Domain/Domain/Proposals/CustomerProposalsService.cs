using Microsoft.EntityFrameworkCore;
using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Common.Model;
using SuperCarga.Application.Domain.Costs.Abstraction;
using SuperCarga.Application.Domain.Costs.Dto;
using SuperCarga.Application.Domain.Drivers.Customers.Dto;
using SuperCarga.Application.Domain.Location.Dto;
using SuperCarga.Application.Domain.Proposals.Customers.Abstraction;
using SuperCarga.Application.Domain.Proposals.Customers.Commands.AddToFavorites;
using SuperCarga.Application.Domain.Proposals.Customers.Commands.RemoveFromFavorites;
using SuperCarga.Application.Domain.Proposals.Customers.Queries.Details;
using SuperCarga.Application.Domain.Proposals.Customers.Queries.Details.Dto;
using SuperCarga.Application.Domain.Proposals.Customers.Queries.List.All;
using SuperCarga.Application.Domain.Proposals.Customers.Queries.List.Common.Dto;
using SuperCarga.Application.Domain.Proposals.Customers.Queries.List.Favorites;
using SuperCarga.Application.Exceptions;
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

        public async Task<ListResponseDto<CustomerProposalListItemDto>> ListAllProposals(CustomerListAllProposalsQuery request) => await ListProposals(request, false);

        public async Task<ListResponseDto<CustomerProposalListItemDto>> ListFavoritesProposals(CustomerListFavoritesProposalsQuery request) => await ListProposals(request, true);

        private async Task<ListResponseDto<CustomerProposalListItemDto>> ListProposals<T>(UserRequest<T, ListResponseDto<CustomerProposalListItemDto>> request, bool onlyFavorite) where T : CustomerListProposalsRequest
        {
            var query = ctx.Proposals
                .Include(x => x.Driver)
                .ThenInclude(x => x.User)
                .Include(x => x.Job)
                .ThenInclude(x => x.Contracts)
                .Where(x => x.Job.CustomerId == request.User.CustomerId)
                .Where(x => !x.Job.Contracts.Where(y => y.ProposalId == x.Id).Any())
                .Select(x => new CustomerProposalListItemDto
                {
                    Id = x.Id,
                    Created = x.Created,
                    PricePerKm = x.PricePerKm,
                    JobId = x.JobId,
                    AddedToFavorite = x.AddedToFavoriteBy.Where(x => x.Id == request.User.CustomerId).Any(),
                    Driver = x.Driver.GetCustomerDriverDto(),
                    State = x.State
                })
                .AsQueryable();

            if(onlyFavorite)
            {
                query = query.Where(x => x.AddedToFavorite).AsQueryable();
            }

            if(request.Data.JobId != null)
            {
                query = query.Where(x => x.JobId == request.Data.JobId).AsQueryable();
            }

            if (request.Data.CreatedFrom != null)
            {
                query = query.Where(x => x.Created >= request.Data.CreatedFrom.Value).AsQueryable();
            }

            if (request.Data.CreatedTo != null)
            {
                query = query.Where(x => x.Created <= request.Data.CreatedTo.Value).AsQueryable();
            }

            if (!string.IsNullOrWhiteSpace(request.Data.State))
            {
                query = query.Where(x => x.State == request.Data.State).AsQueryable();
            }

            if (request.Data.PricePerKmFrom != null)
            {
                query = query.Where(x => x.PricePerKm >= request.Data.PricePerKmFrom.Value).AsQueryable();
            }

            if (request.Data.PricePerKmTo != null)
            {
                query = query.Where(x => x.PricePerKm <= request.Data.PricePerKmTo.Value).AsQueryable();
            }

            var proposals = await query
                .OrderByDescending(x => x.Created)
                .Paginate(request.Data);

            return proposals;
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
