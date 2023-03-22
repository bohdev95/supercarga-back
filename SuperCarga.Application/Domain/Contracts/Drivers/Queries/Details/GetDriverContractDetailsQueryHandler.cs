using MediatR;
using SuperCarga.Application.Domain.Contracts.Drivers.Abstraction;

namespace SuperCarga.Application.Domain.Contracts.Drivers.Queries.Details
{
    public class GetDriverContractDetailsQueryHandler : IRequestHandler<GetDriverContractDetailsQuery, GetDriverContractDetailsQueryResponse>
    {
        private readonly IDriverContractsService driverContractsService;

        public GetDriverContractDetailsQueryHandler(IDriverContractsService driverContractsService)
        {
            this.driverContractsService = driverContractsService;
        }

        public async Task<GetDriverContractDetailsQueryResponse> Handle(GetDriverContractDetailsQuery request, CancellationToken cancellationToken)
        {
            var response = new GetDriverContractDetailsQueryResponse();

            response.Contract = await driverContractsService.GetContractDetails(request);

            return response;
        }
    }
}
