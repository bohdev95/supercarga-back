using MediatR;
using SuperCarga.Application.Domain.Drivers.Drivers.Abstraction;

namespace SuperCarga.Application.Domain.Drivers.Drivers.Queries.Details
{
    public class GetDriverDetailsQueryHandler : IRequestHandler<GetDriverDetailsQuery, GetDriverDetailsQueryResponse>
    {
        private readonly IDriversDriversService driversService;

        public GetDriverDetailsQueryHandler(IDriversDriversService driversService)
        {
            this.driversService = driversService;
        }

        public async Task<GetDriverDetailsQueryResponse> Handle(GetDriverDetailsQuery request, CancellationToken cancellationToken)
        {
            var response = new GetDriverDetailsQueryResponse();

            response.Driver = await driversService.GetDriverDetails(request);

            return response;
        }
    }
}
