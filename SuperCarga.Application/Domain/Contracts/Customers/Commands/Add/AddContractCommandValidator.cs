using FluentValidation;
using SuperCarga.Application.Domain.Proposals.Common.Validation;
using SuperCarga.Application.Validation;

namespace SuperCarga.Application.Domain.Contracts.Customers.Commands.Add
{
    public class AddContractCommandValidator : AbstractValidator<AddContractCommand>
    {
        public AddContractCommandValidator(ProposalExistsValidator proposalExistsValidator, ProposalIsPendingValidator proposalIsPendingValidator)
        {
            RuleFor(c => c.Data.ProposalId).NotEmpty().WithMessage(ValidationMessage.NotEmpty("ProposalId"));
            RuleFor(c => c.Data.ProposalId).SetValidator(proposalExistsValidator);
            RuleFor(c => c.Data.ProposalId).SetValidator(proposalIsPendingValidator);
        }
    }
}
