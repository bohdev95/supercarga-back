using FluentValidation;
using SuperCarga.Application.Domain.Contracts.Common.Validation;
using SuperCarga.Application.Validation;

namespace SuperCarga.Application.Domain.Contracts.Customers.Commands.Finalize
{
    public class CustomerFinalizeCommandValidator : AbstractValidator<CustomerFinalizeCommand>
    {
        public CustomerFinalizeCommandValidator(ContractExistsValidator contractExistsValidator, ContractDeliveryConfirmedValidator contractDeliveryConfirmedValidator)
        {
            RuleFor(c => c.Data.ContractId).NotEmpty().WithMessage(ValidationMessage.NotEmpty("ContractId"));
            RuleFor(c => c.Data.ContractId).SetValidator(contractExistsValidator);
            RuleFor(c => c.Data.ContractId).SetValidator(contractDeliveryConfirmedValidator);
            RuleFor(c => c.Data.Rating).NotEmpty().WithMessage(ValidationMessage.NotEmpty("Rating"));
        }
    }
}
