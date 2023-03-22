using MediatR;
using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Contracts.Common.Dto;
using SuperCarga.Application.Domain.Contracts.Customers.Abstraction;

namespace SuperCarga.Application.Domain.Contracts.Customers.Queries.List.Finished
{
    public class CustomerListFinishedContractsQueryHandler : IRequestHandler<CustomerListFinishedContractsQuery, ListResponseDto<FinishedContractListITemDto>>
    {
        private readonly ICustomerContractsService customerContractsService;

        public CustomerListFinishedContractsQueryHandler(ICustomerContractsService customerContractsService)
        {
            this.customerContractsService = customerContractsService;
        }

        public async Task<ListResponseDto<FinishedContractListITemDto>> Handle(CustomerListFinishedContractsQuery request, CancellationToken cancellationToken)
        {
            var response = await customerContractsService.ListFinishedContracts(request);

            return response;
        }
    }
}
