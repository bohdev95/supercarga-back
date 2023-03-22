using MediatR;
using SuperCarga.Application.Domain.Proposals.Customers.Abstraction;
using SuperCarga.Application.Exceptions;

namespace SuperCarga.Application.Domain.Proposals.Customers.Commands.RemoveFromFavorites
{
    public class RemoveProposalFromFavoritesCommandHandrer : IRequestHandler<RemoveProposalFromFavoritesCommand, RemoveProposalFromFavoritesCommandResponse>
    {
        private readonly RemoveProposalFromFavoritesCommandValidator validator;
        private readonly ICustomerProposalsService customerProposalsService;

        public RemoveProposalFromFavoritesCommandHandrer(RemoveProposalFromFavoritesCommandValidator validator, ICustomerProposalsService customerProposalsService)
        {
            this.validator = validator;
            this.customerProposalsService = customerProposalsService;
        }

        public async Task<RemoveProposalFromFavoritesCommandResponse> Handle(RemoveProposalFromFavoritesCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request);
            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            var response = new RemoveProposalFromFavoritesCommandResponse();

            await customerProposalsService.RemoveProposalFromFavorites(request);

            return response;
        }
    }
}
