using FluentValidation;
using SuperCarga.Application.Domain.Contracts.Common.Validation;
using SuperCarga.Application.Validation;

namespace SuperCarga.Application.Domain.Contracts.Customers.Commands.ConfirmDelivery
{
    public class CustomerConfirmDeliveryCommandValidator : AbstractValidator<CustomerConfirmDeliveryCommand>
    {
        public CustomerConfirmDeliveryCommandValidator(ContractExistsValidator contractExistsValidator, ContractDeliveredValidator contractDeliveredValidator)
        {
            RuleFor(c => c.Data.ContractId).NotEmpty().WithMessage(ValidationMessage.NotEmpty("ContractId"));
            RuleFor(c => c.Data.ContractId).SetValidator(contractExistsValidator);
            RuleFor(c => c.Data.ContractId).SetValidator(contractDeliveredValidator);
        }
    }
}
