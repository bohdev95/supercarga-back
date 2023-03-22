using MediatR;
using SuperCarga.Application.Domain.Customers.Customers.Abstraction;
using SuperCarga.Application.Exceptions;

namespace SuperCarga.Application.Domain.Customers.Customers.Commands.Register
{
    public class RegisterCustomerCommandHandler : IRequestHandler<RegisterCustomerCommand, RegisterCustomerCommandResponse>
    {
        private readonly RegisterCustomerCommandValidator validator;
        private readonly ICustomersCustomersService customersService;

        public RegisterCustomerCommandHandler(RegisterCustomerCommandValidator validator, ICustomersCustomersService customersService)
        {
            this.validator = validator;
            this.customersService = customersService;
        }

        public async Task<RegisterCustomerCommandResponse> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request);
            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            var response = new RegisterCustomerCommandResponse();

            response.Id = await customersService.CreateCustomer(request);

            return response;
        }
    }
}
