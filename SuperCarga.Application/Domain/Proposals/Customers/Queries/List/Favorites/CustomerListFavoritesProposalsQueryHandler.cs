using MediatR;
using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Proposals.Customers.Abstraction;
using SuperCarga.Application.Domain.Proposals.Customers.Queries.List.Common.Dto;

namespace SuperCarga.Application.Domain.Proposals.Customers.Queries.List.Favorites
{
    public class CustomerListFavoritesProposalsQueryHandler : IRequestHandler<CustomerListFavoritesProposalsQuery, ListResponseDto<CustomerProposalListItemDto>>
    {
        private readonly ICustomerProposalsService customerProposalsService;

        public CustomerListFavoritesProposalsQueryHandler(ICustomerProposalsService customerProposalsService)
        {
            this.customerProposalsService = customerProposalsService;
        }

        public async Task<ListResponseDto<CustomerProposalListItemDto>> Handle(CustomerListFavoritesProposalsQuery request, CancellationToken cancellationToken)
        {
            var response = await customerProposalsService.ListFavoritesProposals(request);

            return response;
        }
    }
}
