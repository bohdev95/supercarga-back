using MediatR;
using SuperCarga.Application.Domain.Proposals.Drivers.Abstraction;

namespace SuperCarga.Application.Domain.Proposals.Drivers.Queries.Details
{
    public class GetDriverProposalDetailsQueryHandler : IRequestHandler<GetDriverProposalDetailsQuery, GetDriverProposalDetailsQueryResponse>
    {
        private readonly IDriverProposalsService driverProposalsService;

        public GetDriverProposalDetailsQueryHandler(IDriverProposalsService driverProposalsService)
        {
            this.driverProposalsService = driverProposalsService;
        }

        public async Task<GetDriverProposalDetailsQueryResponse> Handle(GetDriverProposalDetailsQuery request, CancellationToken cancellationToken)
        {
            var response = new GetDriverProposalDetailsQueryResponse();

            response.Proposal = await driverProposalsService.GetProposalsDetails(request);

            return response;
        }
    }
}
