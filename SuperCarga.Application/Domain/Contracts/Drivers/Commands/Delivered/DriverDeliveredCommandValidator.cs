using FluentValidation;
using SuperCarga.Application.Domain.Common.Validation;
using SuperCarga.Application.Domain.Contracts.Common.Validation;
using SuperCarga.Application.Validation;

namespace SuperCarga.Application.Domain.Contracts.Drivers.Commands.Delivered
{
    public class DriverDeliveredCommandValidator : AbstractValidator<DriverDeliveredCommand>
    {
        public DriverDeliveredCommandValidator(ContractExistsValidator contractExistsValidator, ContractInProgressValidator contractInProgressValidator, ImageDtoValidator imageValidator)
        {
            RuleFor(c => c.Data.ContractId).NotEmpty().WithMessage(ValidationMessage.NotEmpty("ContractId"));
            RuleFor(c => c.Data.ContractId).SetValidator(contractExistsValidator);
            RuleFor(c => c.Data.ContractId).SetValidator(contractInProgressValidator);

            RuleFor(c => c.Data.CargoImage).NotEmpty().WithMessage(ValidationMessage.NotEmpty("CargoImage"));
            RuleFor(c => c.Data.CargoImage).SetValidator(imageValidator);

            RuleFor(c => c.Data.ProofImage).NotEmpty().WithMessage(ValidationMessage.NotEmpty("ProofImage"));
            RuleFor(c => c.Data.ProofImage).SetValidator(imageValidator);
        }
    }
}
