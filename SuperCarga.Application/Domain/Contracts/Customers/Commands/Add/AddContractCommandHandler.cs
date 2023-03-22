using MediatR;
using SuperCarga.Application.Domain.Contracts.Customers.Abstraction;
using SuperCarga.Application.Exceptions;

namespace SuperCarga.Application.Domain.Contracts.Customers.Commands.Add
{
    public class AddContractCommandHandler : IRequestHandler<AddContractCommand, AddContractCommandResponse>
    {
        private readonly AddContractCommandValidator validator;
        private readonly ICustomerContractsService customerContractsService;

        public AddContractCommandHandler(AddContractCommandValidator validator, ICustomerContractsService customerContractsService)
        {
            this.validator = validator;
            this.customerContractsService = customerContractsService;
        }

        public async Task<AddContractCommandResponse> Handle(AddContractCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request);
            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            var response = new AddContractCommandResponse();

            response.ContractId = await customerContractsService.AddContract(request);

            return response;
        }
    }
}
