using FluentValidation;
using SuperCarga.Application.Domain.Jobs.Common.Validation;
using SuperCarga.Application.Domain.Proposals.Common.Validation;
using SuperCarga.Application.Validation;

namespace SuperCarga.Application.Domain.Proposals.Customers.Commands.RemoveFromFavorites
{
    public class RemoveProposalFromFavoritesCommandValidator : AbstractValidator<RemoveProposalFromFavoritesCommand>
    {
        public RemoveProposalFromFavoritesCommandValidator(ProposalExistsValidator proposalExistsValidator)
        {
            RuleFor(c => c.Data.ProposalId).NotEmpty().WithMessage(ValidationMessage.NotEmpty("ProposalId"));
            RuleFor(c => c.Data.ProposalId).SetValidator(proposalExistsValidator);
        }
    }

}
