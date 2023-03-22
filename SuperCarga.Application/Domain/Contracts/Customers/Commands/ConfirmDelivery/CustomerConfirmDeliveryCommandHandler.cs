using MediatR;
using SuperCarga.Application.Domain.Contracts.Customers.Abstraction;
using SuperCarga.Application.Exceptions;

namespace SuperCarga.Application.Domain.Contracts.Customers.Commands.ConfirmDelivery
{
    public class CustomerConfirmDeliveryCommandHandler : IRequestHandler<CustomerConfirmDeliveryCommand, CustomerConfirmDeliveryCommandResponse>
    {
        private readonly CustomerConfirmDeliveryCommandValidator validator;
        private readonly ICustomerContractsService customerContractsService;

        public CustomerConfirmDeliveryCommandHandler(CustomerConfirmDeliveryCommandValidator validator, ICustomerContractsService customerContractsService)
        {
            this.validator = validator;
            this.customerContractsService = customerContractsService;
        }

        public async Task<CustomerConfirmDeliveryCommandResponse> Handle(CustomerConfirmDeliveryCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request);
            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            var response = new CustomerConfirmDeliveryCommandResponse();

            await customerContractsService.ConfirmDelivery(request);

            return response;
        }
    }
}
