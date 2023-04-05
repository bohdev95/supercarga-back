using Microsoft.EntityFrameworkCore;
using SuperCarga.Application.Domain.Common.Abstraction;
using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Common.Model;
using SuperCarga.Application.Domain.Costs.Abstraction;
using SuperCarga.Application.Domain.Costs.Dto;
using SuperCarga.Application.Domain.Jobs.Common.Models;
using SuperCarga.Application.Domain.Jobs.Customers.Abstraction;
using SuperCarga.Application.Domain.Jobs.Customers.Commands.Add;
using SuperCarga.Application.Domain.Jobs.Customers.Commands.Close;
using SuperCarga.Application.Domain.Jobs.Customers.Queries.Details;
using SuperCarga.Application.Domain.Jobs.Customers.Queries.Details.Dto;
using SuperCarga.Application.Domain.Jobs.Customers.Queries.List.Active;
using SuperCarga.Application.Domain.Jobs.Customers.Queries.List.Archived;
using SuperCarga.Application.Domain.Jobs.Customers.Queries.List.Common.Dto;
using SuperCarga.Application.Domain.Location.Dto;
using SuperCarga.Application.Domain.Proposals.Common.Models;
using SuperCarga.Application.Exceptions;
using SuperCarga.Domain.Domain.Common;
using SuperCarga.Persistence.Database;

namespace SuperCarga.Domain.Domain.Jobs
{
    public class CustomerJobsService : ICustomerJobsService
    {
        private readonly SuperCargaContext ctx;
        private readonly ICostsService costsService;
        private readonly IImagesService imagesService;

        public CustomerJobsService(SuperCargaContext ctx, ICostsService costsService, IImagesService imagesService)
        {
            this.ctx = ctx;
            this.costsService = costsService;
            this.imagesService = imagesService;
        }
        public async Task<CustomerJobsDetailsDto> GetJobsDetails(GetCustomerJobsDetailsQuery request)
        {
            var jobWasCreatedByCustomer = await ctx.Jobs
                .Where(x => x.Id == request.Data.JobId)
                .Where(x => x.CustomerId == request.User.CustomerId.Value)
                .AnyAsync();

            if (!jobWasCreatedByCustomer)
            {
                throw new ForbiddenException();
            }

            var dto = await ctx.Jobs
                .Include(x => x.VehiculeType)
                .Include(x => x.Proposals)
                .Include(x => x.Additions)
                .Where(x => x.Id == request.Data.JobId)
                .Select(x => new CustomerJobsDetailsDto
                {
                    Id = x.Id,
                    Created = x.Created,
                    VehiculeTypeName = x.VehiculeType.Name,
                    RequireLoadingCrew = x.RequireLoadingCrew,
                    RequireUnloadingCrew = x.RequireUnloadingCrew,
                    Description = x.Description,
                    Tittle = x.Tittle,
                    Distance = x.Distance,
                    PickupDate = x.PickupDate,
                    DeliveryDate = x.DeliveryDate,
                    Cargo = x.GetCargo(),
                    Origin = x.GetOrigin(),
                    Destination = x.GetDestination(),
                    CostsSummary = x.GetCostsSummary(),
                    State = x.State
                })
                .FirstOrDefaultAsync();

            return dto;
        }

        public async Task<Guid> AddJob(AddJobCommand request)
        {
            var costs = await costsService.CalculateCostsSummary(new CaluclateCostsSummaryDto
            {
                PricePerKm = request.Data.PricePerKm,
                Distance = request.Data.Distance,
                RequireLoadingCrew = request.Data.RequireLoadingCrew,
                RequireUnloadingCrew = request.Data.RequireUnloadingCrew
            });

            var job = new Job
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                CustomerId = request.User.CustomerId.Value,
                Tittle = request.Data.Tittle,
                Description = request.Data.Description,
                PickupDate = request.Data.PickupDate,
                DeliveryDate = request.Data.DeliveryDate,
                CargoWeight = request.Data.Cargo.Weight,
                CargoWidth = request.Data.Cargo.Width,
                CargoHeight = request.Data.Cargo.Height,
                CargoLenght = request.Data.Cargo.Lenght,
                VehiculeTypeId = request.Data.VehiculeTypeId,
                OriginCity = request.Data.Origin.City,
                OriginStreet = request.Data.Origin.Street,
                OriginPostCode = request.Data.Origin.PostCode,
                DestinationCity = request.Data.Destination.City,
                DestinationStreet = request.Data.Destination.Street,
                DestinationPostCode = request.Data.Destination.PostCode,
                Distance = request.Data.Distance,
                State = JobState.Active,
                RequireLoadingCrew = request.Data.RequireLoadingCrew,
                RequireUnloadingCrew = request.Data.RequireUnloadingCrew,
                PricePerKm = costs.PricePerKm,
                PricePerDistance = costs.PricePerDistance,
                TotalPrice = costs.TotalPrice,
                ServiceFee = costs.ServiceFee,
                Price = costs.Price
            };

            if (request.Data.Cargo.Image != null)
            {
                job.CargoImagePath = await imagesService
                    .SaveCargoImage(request.Data.Cargo.Image, job.Id);
            }

            var additions = costs.Additions.Select(x => new JobAdditionalCost
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                JobId = job.Id,
                Name = x.Name,
                Price = x.Price
            })
            .ToList();

            await ctx.AddAsync(job);
            await ctx.JobAdditionalCosts.AddRangeAsync(additions);
            await ctx.SaveChangesAsync();

            return job.Id;
        }

        public async Task CloseJob(CloseJobCommand request)
        {
            var job = await ctx.Jobs
                .Include(x => x.Proposals)
                .Where(x => x.Id == request.Data.JobId)
                .FirstOrDefaultAsync();

            if(job.CustomerId != request.User.CustomerId.Value)
            {
                throw new ForbiddenException();
            }

            job.State = JobState.Closed;
            job.Proposals.ForEach(x => x.State = ProposalState.Closed);

            await ctx.SaveChangesAsync();
        }

        public async Task<ListResponseDto<CustomerJobListItemDto>> ListActiveJobs(CustomerListActiveJobsQuery request) => await ListJobs(request, JobState.Active);

        public async Task<ListResponseDto<CustomerJobListItemDto>> ListArchivedJobs(CustomerListArchivedJobsQuery request) => await ListJobs(request, JobState.Closed);

        private async Task<ListResponseDto<CustomerJobListItemDto>> ListJobs<T>(UserRequest<T, ListResponseDto<CustomerJobListItemDto>> request, string state) where T : CustomerListJobRequest
        {
            var query = ctx.Jobs
                .Include(x => x.Proposals)
                .Include(x => x.Contracts)
                .Where(x => x.CustomerId == request.User.CustomerId)
                .Where(x => x.State == state)
                .Select(x => new CustomerJobListItemDto
                {
                    Id = x.Id,
                    Created = x.Created,
                    Tittle = x.Tittle,
                    Proposals = x.Proposals.Count(),
                    NewProposals = x.Proposals.Where(y => !y.Checked).Count(),
                    Hired = x.Contracts.Count()
                })
                .AsQueryable();

            if(request.Data.CreatedFrom != null)
            {
                query = query.Where(x => x.Created.Date >= request.Data.CreatedFrom.Value.Date).AsQueryable();
            }

            if (request.Data.CreatedTo != null)
            {
                query = query.Where(x => x.Created.Date <= request.Data.CreatedTo.Value.Date).AsQueryable();
            }

            if (!string.IsNullOrWhiteSpace(request.Data.Search))
            {
                var search = request.Data.Search.ToLower().Trim();

                query = query.Where(x => x.Tittle.Contains(search)).AsQueryable();
            }

            var jobs = await query
                .OrderByDescending(x => x.Created)
                .Paginate(request.Data);

            return jobs;
        }

    }
}
