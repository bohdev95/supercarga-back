using FluentValidation;
using SuperCarga.Application.Domain.Common.Validation;
using SuperCarga.Application.Domain.Contracts.Common.Validation;
using SuperCarga.Application.Validation;

namespace SuperCarga.Application.Domain.Contracts.Drivers.Commands.PickedUp
{
    public class DriverPickedUpCommandValidator : AbstractValidator<DriverPickedUpCommand>
    {
        public DriverPickedUpCommandValidator(ContractExistsValidator contractExistsValidator, ContractStartedValidator contractStartedValidator, ImageDtoValidator imageValidator)
        {
            RuleFor(c => c.Data.ContractId).NotEmpty().WithMessage(ValidationMessage.NotEmpty("ContractId"));
            RuleFor(c => c.Data.ContractId).SetValidator(contractExistsValidator);
            RuleFor(c => c.Data.ContractId).SetValidator(contractStartedValidator);

            RuleFor(c => c.Data.CargoImage).NotEmpty().WithMessage(ValidationMessage.NotEmpty("CargoImage"));
            RuleFor(c => c.Data.CargoImage).SetValidator(imageValidator);

            RuleFor(c => c.Data.ProofImage).NotEmpty().WithMessage(ValidationMessage.NotEmpty("ProofImage"));
            RuleFor(c => c.Data.ProofImage).SetValidator(imageValidator);
        }
    }
}
