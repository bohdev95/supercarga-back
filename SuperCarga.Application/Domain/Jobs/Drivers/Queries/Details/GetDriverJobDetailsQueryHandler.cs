using MediatR;
using SuperCarga.Application.Domain.Jobs.Drivers.Abstraction;

namespace SuperCarga.Application.Domain.Jobs.Drivers.Queries.Details
{
    public class GetDriverJobsDetailsQueryHandler : IRequestHandler<GetDriverJobsDetailsQuery, GetDriverJobsDetailsQueryResponse>
    {
        private readonly IDriverJobsService driverJobsService;

        public GetDriverJobsDetailsQueryHandler(IDriverJobsService driverJobsService)
        {
            this.driverJobsService = driverJobsService;
        }

        public async Task<GetDriverJobsDetailsQueryResponse> Handle(GetDriverJobsDetailsQuery request, CancellationToken cancellationToken)
        {
            var response = new GetDriverJobsDetailsQueryResponse();

            response.Job = await driverJobsService.GetJobsDetails(request);

            return response;
        }
    }
}
