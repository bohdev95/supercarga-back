using FluentValidation;
using SuperCarga.Application.Domain.Proposals.Common.Validation;
using SuperCarga.Application.Validation;

namespace SuperCarga.Application.Domain.Contracts.Drivers.Commands.Start
{
    public class StartContractCommandValidator : AbstractValidator<StartContractCommand>
    {
        public StartContractCommandValidator(ProposalExistsValidator proposalExistsValidator, ProposalIsAcceptedValidator proposalIsAcceptedValidator)
        {
            RuleFor(c => c.Data.ProposalId).NotEmpty().WithMessage(ValidationMessage.NotEmpty("ProposalId"));
            RuleFor(c => c.Data.ProposalId).SetValidator(proposalExistsValidator);
            RuleFor(c => c.Data.ProposalId).SetValidator(proposalIsAcceptedValidator);
        }
    }
}
