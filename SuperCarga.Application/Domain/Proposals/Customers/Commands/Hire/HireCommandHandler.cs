using MediatR;
using SuperCarga.Application.Domain.Proposals.Customers.Abstraction;
using SuperCarga.Application.Exceptions;

namespace SuperCarga.Application.Domain.Proposals.Customers.Commands.Hire
{
    public class HireCommandHandler : IRequestHandler<HireCommand, HireCommandResponse>
    {
        private readonly HireCommandValidator validator;
        private readonly ICustomerProposalsService customerProposalsService;

        public HireCommandHandler(HireCommandValidator validator, ICustomerProposalsService customerProposalsService)
        {
            this.validator = validator;
            this.customerProposalsService = customerProposalsService;
        }

        public async Task<HireCommandResponse> Handle(HireCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request);
            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            var response = new HireCommandResponse();

            response.ContractId = await customerProposalsService.Hire(request);

            return response;
        }
    }
}
