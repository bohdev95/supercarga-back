using MediatR;
using SuperCarga.Application.Domain.Contracts.Customers.Abstraction;
using SuperCarga.Application.Exceptions;

namespace SuperCarga.Application.Domain.Contracts.Customers.Commands.Finalize
{
    public class CustomerFinalizeCommandHandler : IRequestHandler<CustomerFinalizeCommand, CustomerFinalizeCommandResponse>
    {
        private readonly CustomerFinalizeCommandValidator validator;
        private readonly ICustomerContractsService customerContractsService;

        public CustomerFinalizeCommandHandler(CustomerFinalizeCommandValidator validator, ICustomerContractsService customerContractsService)
        {
            this.validator = validator;
            this.customerContractsService = customerContractsService;
        }

        public async Task<CustomerFinalizeCommandResponse> Handle(CustomerFinalizeCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request);
            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            var response = new CustomerFinalizeCommandResponse();

            await customerContractsService.Finalize(request);

            return response;
        }
    }
}
