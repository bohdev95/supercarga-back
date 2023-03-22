using MediatR;
using SuperCarga.Application.Domain.Proposals.Customers.Abstraction;

namespace SuperCarga.Application.Domain.Proposals.Customers.Queries.Details
{
    public class GetCustomerProposalDetailsQueryHandler : IRequestHandler<GetCustomerProposalDetailsQuery, GetCustomerProposalDetailsQueryResponse>
    {
        private readonly ICustomerProposalsService customerProposalsService;

        public GetCustomerProposalDetailsQueryHandler(ICustomerProposalsService customerProposalsService)
        {
            this.customerProposalsService = customerProposalsService;
        }

        public async Task<GetCustomerProposalDetailsQueryResponse> Handle(GetCustomerProposalDetailsQuery request, CancellationToken cancellationToken)
        {
            var response = new GetCustomerProposalDetailsQueryResponse();

            response.Proposal = await customerProposalsService.GetProposalsDetails(request);

            return response;
        }
    }
}
