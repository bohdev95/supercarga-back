using MediatR;
using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Contracts.Common.Dto;
using SuperCarga.Application.Domain.Contracts.Customers.Abstraction;

namespace SuperCarga.Application.Domain.Contracts.Customers.Queries.List.Active
{
    public class CustomerListActiveContractsQueryHandler : IRequestHandler<CustomerListActiveContractsQuery, ListResponseDto<ActiveContractListITemDto>>
    {
        private readonly ICustomerContractsService customerContractsService;

        public CustomerListActiveContractsQueryHandler(ICustomerContractsService customerContractsService)
        {
            this.customerContractsService = customerContractsService;
        }

        public async Task<ListResponseDto<ActiveContractListITemDto>> Handle(CustomerListActiveContractsQuery request, CancellationToken cancellationToken)
        {
            var response = await customerContractsService.ListActiveContracts(request);

            return response;
        }
    }
}
