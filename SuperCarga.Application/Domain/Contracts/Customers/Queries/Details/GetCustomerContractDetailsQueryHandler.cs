using MediatR;
using SuperCarga.Application.Domain.Contracts.Customers.Abstraction;
using SuperCarga.Application.Domain.Contracts.Drivers.Abstraction;

namespace SuperCarga.Application.Domain.Contracts.Customers.Queries.Details
{
    public class GetCustomerContractDetailsQueryHandler : IRequestHandler<GetCustomerContractDetailsQuery, GetCustomerContractDetailsQueryResponse>
    {
        private readonly ICustomerContractsService customerContractsService;

        public GetCustomerContractDetailsQueryHandler(ICustomerContractsService customerContractsService)
        {
            this.customerContractsService = customerContractsService;
        }

        public async Task<GetCustomerContractDetailsQueryResponse> Handle(GetCustomerContractDetailsQuery request, CancellationToken cancellationToken)
        {
            var response = new GetCustomerContractDetailsQueryResponse();

            response.Contract = await customerContractsService.GetContractDetails(request);

            return response;
        }
    }
}
