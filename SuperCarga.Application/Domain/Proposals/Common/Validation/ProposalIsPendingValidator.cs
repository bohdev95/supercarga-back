using SuperCarga.Application.Domain.Proposals.Common.Abstraction;
using SuperCarga.Application.Domain.Proposals.Common.Models;

namespace SuperCarga.Application.Domain.Proposals.Common.Validation
{
    public class ProposalIsPendingValidator : ProposalStateValidator
    {
        public ProposalIsPendingValidator(IProposalsService proposalsService) : base(proposalsService, ProposalState.Pending)
        {
        }
    }
}
