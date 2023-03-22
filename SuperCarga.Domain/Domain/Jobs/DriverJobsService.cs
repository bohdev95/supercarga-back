using Microsoft.EntityFrameworkCore;
using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Customers.Driver.Dto;
using SuperCarga.Application.Domain.Jobs.Drivers.Abstraction;
using SuperCarga.Application.Domain.Jobs.Drivers.Commands.AddToFavorites;
using SuperCarga.Application.Domain.Jobs.Drivers.Commands.RemoveFromFavorites;
using SuperCarga.Application.Domain.Jobs.Drivers.Queries.Details;
using SuperCarga.Application.Domain.Jobs.Drivers.Queries.Details.Dto;
using SuperCarga.Application.Domain.Jobs.Drivers.Queries.List.All;
using SuperCarga.Application.Domain.Jobs.Drivers.Queries.List.Common.Dto;
using SuperCarga.Application.Domain.Jobs.Drivers.Queries.List.Favorites;
using SuperCarga.Application.Domain.Location.Dto;
using SuperCarga.Domain.Domain.Common;
using SuperCarga.Persistence.Database;

namespace SuperCarga.Domain.Domain.Jobs
{
    public class DriverJobsService : IDriverJobsService
    {
        private readonly SuperCargaContext ctx;

        public DriverJobsService(SuperCargaContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task<DriverJobsDetailsDto> GetJobsDetails(GetDriverJobsDetailsQuery request)
        {
            var dto = await ctx.Jobs
                .Include(x => x.VehiculeType)
                .Include(x => x.Proposals)
                .Include(x => x.Customer)
                .ThenInclude(x => x.User)
                .Where(x => x.Id == request.Data.JobId)
                .Select(x => new
                {
                    job = x,
                    proposalAlreadyAdded = x.Proposals.Where(x => x.DriverId == request.User.DriverId).Any()
                })
                .Select(x => new DriverJobsDetailsDto
                {
                    Id = x.job.Id,
                    Created = x.job.Created,
                    VehiculeTypeName = x.job.VehiculeType.Name,
                    RequireLoadingCrew = x.job.RequireLoadingCrew,
                    RequireUnloadingCrew = x.job.RequireUnloadingCrew,
                    PrpoposalAlreadyAdded = x.proposalAlreadyAdded,
                    Description = x.job.Description,
                    Tittle = x.job.Tittle,
                    Distance = x.job.Distance,
                    PricePerKm = x.job.PricePerKm,
                    PickupDate = x.job.PickupDate,
                    DeliveryDate = x.job.DeliveryDate,
                    Cargo = x.job.GetCargo(),
                    Origin = x.job.GetOrigin(),
                    Destination = x.job.GetDestination(),
                    Customer = x.job.Customer.GetDriverCustomerDto(),
                    State = x.job.State
                })
                .FirstOrDefaultAsync();

            return dto;
        }

        public async Task AddJobToFavorites(AddJobToFavoritesCommand request)
        {
            var driver = await ctx.Drivers
                .Include(x => x.FavoriteJobs)
                .Where(x => x.Id == request.User.DriverId.Value)
                .FirstOrDefaultAsync();

            var alreadyAdded = driver.FavoriteJobs
                .Where(x => x.Id == request.Data.JobId).Any();

            if (!alreadyAdded)
            {
                var job = await ctx.Jobs
                    .Where(x => x.Id == request.Data.JobId)
                    .FirstOrDefaultAsync();

                driver.FavoriteJobs.Add(job);

                await ctx.SaveChangesAsync();
            }
        }

        public async Task RemoveJobFromFavorites(RemoveJobFromFavoritesCommand request)
        {
            var driver = await ctx.Drivers
                .Include(x => x.FavoriteJobs)
                .Where(x => x.Id == request.User.DriverId.Value)
                .FirstOrDefaultAsync();

            driver.FavoriteJobs.RemoveAll(x => x.Id == request.Data.JobId);

            await ctx.SaveChangesAsync();
        }

        public async Task<ListResponseDto<DriverJobListItemDto>> ListAllJobs(DriverListAllJobsQuery request)
        {
            var query = await ListJobs(request.User.DriverId.Value);

            var res = await query
                .Paginate(request.Data);

            return res;
        }

        public async Task<ListResponseDto<DriverJobListItemDto>> ListFavoritesJobs(DriverListFavoritesJobsQuery request)
        {
            var query = await ListJobs(request.User.DriverId.Value);

            var res = await query
                .Where(x => x.AddedToFavorite)
                .Paginate(request.Data);

            return res;
        }

        private async Task<IQueryable<DriverJobListItemDto>> ListJobs(Guid driverId)
        {
            var driverVehiculeTypeId = await ctx.Drivers
                .Where(x => x.Id == driverId)
                .Select(x => x.VehiculeTypeId)
                .FirstOrDefaultAsync();

            var query = ctx.Jobs
                .Include(x => x.VehiculeType)
                .Include(x => x.Proposals)
                .Include(x => x.Customer)
                .ThenInclude(x => x.User)
                .Where(x => x.VehiculeTypeId == driverVehiculeTypeId)
                .Select(x => new
                {
                    job = x,
                    proposalAlreadyAdded = x.Proposals.Where(x => x.DriverId == driverId).Any(),
                    addedToFavorite = x.AddedToFavoriteBy.Where(x => x.Id == driverId).Any()
                })
                .Select(x => new DriverJobListItemDto
                {
                    Id = x.job.Id,
                    Created = x.job.Created,
                    Tittle = x.job.Tittle,
                    VehiculeTypeName = x.job.VehiculeType.Name,
                    PricePerKm = x.job.PricePerKm,
                    PrpoposalAlreadyAdded = x.proposalAlreadyAdded,
                    AddedToFavorite = x.addedToFavorite,
                    PickupDate = x.job.PickupDate,
                    Distance = x.job.Distance,
                    Origin = x.job.GetOrigin(),
                    Destination = x.job.GetDestination(),
                    Customer = x.job.Customer.GetDriverCustomerDto()
                })
                .OrderByDescending(x => x.Created)
                .AsQueryable();

            return query;
        }
    }
}
