using SuperCarga.Application.Domain.Proposals.Common.Abstraction;
using SuperCarga.Application.Domain.Proposals.Common.Models;

namespace SuperCarga.Application.Domain.Proposals.Common.Validation
{
    public class ProposalIsAcceptedValidator : ProposalStateValidator
    {
        public ProposalIsAcceptedValidator(IProposalsService proposalsService) : base(proposalsService, ProposalState.Accepted)
        {
        }
    }
}
