using MediatR;
using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Proposals.Customers.Abstraction;
using SuperCarga.Application.Domain.Proposals.Customers.Queries.List.Common.Dto;

namespace SuperCarga.Application.Domain.Proposals.Customers.Queries.List.All
{
    public class CustomerListAllProposalsQueryHandler : IRequestHandler<CustomerListAllProposalsQuery, ListResponseDto<CustomerProposalListItemDto>>
    {
        private readonly ICustomerProposalsService customerProposalsService;

        public CustomerListAllProposalsQueryHandler(ICustomerProposalsService customerProposalsService)
        {
            this.customerProposalsService = customerProposalsService;
        }

        public async Task<ListResponseDto<CustomerProposalListItemDto>> Handle(CustomerListAllProposalsQuery request, CancellationToken cancellationToken)
        {
            var response = await customerProposalsService.ListAllProposals(request);

            return response;
        }
    }
}
