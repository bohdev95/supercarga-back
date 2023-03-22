using MediatR;
using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Jobs.Drivers.Abstraction;
using SuperCarga.Application.Domain.Jobs.Drivers.Queries.List.Common.Dto;

namespace SuperCarga.Application.Domain.Jobs.Drivers.Queries.List.All
{
    public class DriverListAllJobsQueryHandler : IRequestHandler<DriverListAllJobsQuery, ListResponseDto<DriverJobListItemDto>>
    {
        private readonly IDriverJobsService driverJobsService;

        public DriverListAllJobsQueryHandler(IDriverJobsService driverJobsService)
        {
            this.driverJobsService = driverJobsService;
        }

        public async Task<ListResponseDto<DriverJobListItemDto>> Handle(DriverListAllJobsQuery request, CancellationToken cancellationToken)
        {
            var response = await driverJobsService.ListAllJobs(request);

            return response;
        }
    }
}
