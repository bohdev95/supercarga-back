using FluentValidation;
using SuperCarga.Application.Domain.Proposals.Common.Validation;
using SuperCarga.Application.Validation;

namespace SuperCarga.Application.Domain.Proposals.Customers.Commands.AddToFavorites
{
    public class AddProposalToFavoritesCommandValidator : AbstractValidator<AddProposalToFavoritesCommand>
    {
        public AddProposalToFavoritesCommandValidator(ProposalExistsValidator proposalExistsValidator)
        {
            RuleFor(c => c.Data.ProposalId).NotEmpty().WithMessage(ValidationMessage.NotEmpty("ProposalId"));
            RuleFor(c => c.Data.ProposalId).SetValidator(proposalExistsValidator);
        }
    }
}
