using FluentValidation;
using SuperCarga.Application.Domain.Proposals.Common.Abstraction;
using SuperCarga.Application.Validation;

namespace SuperCarga.Application.Domain.Proposals.Common.Validation
{
    public abstract class ProposalStateValidator : AbstractValidator<Guid>
    {
        public ProposalStateValidator(IProposalsService proposalsService, string proposalState)
        {
            RuleFor(x => x).Custom((proposalId, ctx) =>
            {
                var actualProposalState = proposalsService.GetState(proposalId);

                if (actualProposalState != proposalState)
                    ctx.AddFailure("proposalId", ValidationMessage.InvalidState("Proposal"));
            });
        }
    }
}
