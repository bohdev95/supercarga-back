using MediatR;
using SuperCarga.Application.Domain.Proposals.Customers.Abstraction;
using SuperCarga.Application.Exceptions;

namespace SuperCarga.Application.Domain.Proposals.Customers.Commands.AddToFavorites
{
    public class AddProposalToFavoritesCommandHandrer : IRequestHandler<AddProposalToFavoritesCommand, AddProposalToFavoritesCommandResponse>
    {
        private readonly AddProposalToFavoritesCommandValidator validator;
        private readonly ICustomerProposalsService customerProposalsService;

        public AddProposalToFavoritesCommandHandrer(AddProposalToFavoritesCommandValidator validator, ICustomerProposalsService customerProposalsService)
        {
            this.validator = validator;
            this.customerProposalsService = customerProposalsService;
        }

        public async Task<AddProposalToFavoritesCommandResponse> Handle(AddProposalToFavoritesCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request);
            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            var response = new AddProposalToFavoritesCommandResponse();

            await customerProposalsService.AddProposalToFavorites(request);

            return response;
        }
    }
}
