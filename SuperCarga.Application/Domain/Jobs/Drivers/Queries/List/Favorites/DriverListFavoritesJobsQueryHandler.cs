using MediatR;
using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Jobs.Drivers.Abstraction;
using SuperCarga.Application.Domain.Jobs.Drivers.Queries.List.Common.Dto;

namespace SuperCarga.Application.Domain.Jobs.Drivers.Queries.List.Favorites
{
    public class DriverListFavoritesJobsQueryHandler : IRequestHandler<DriverListFavoritesJobsQuery, ListResponseDto<DriverJobListItemDto>>
    {
        private readonly IDriverJobsService driverJobsService;

        public DriverListFavoritesJobsQueryHandler(IDriverJobsService driverJobsService)
        {
            this.driverJobsService = driverJobsService;
        }

        public async Task<ListResponseDto<DriverJobListItemDto>> Handle(DriverListFavoritesJobsQuery request, CancellationToken cancellationToken)
        {
            var response = await driverJobsService.ListFavoritesJobs(request);

            return response;
        }
    }
}
