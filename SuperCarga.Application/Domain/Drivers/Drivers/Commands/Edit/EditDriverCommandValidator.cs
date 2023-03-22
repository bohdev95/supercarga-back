using FluentValidation;
using SuperCarga.Application.Domain.Drivers.Common.Validation;
using SuperCarga.Application.Domain.VehiculeTypes.Validation;
using SuperCarga.Application.Validation;

namespace SuperCarga.Application.Domain.Drivers.Drivers.Commands.Edit
{
    public class EditDriverCommandValidator : AbstractValidator<EditDriverCommand>
    {
        public EditDriverCommandValidator(DriverNotExistsValidator driverNotExistsValidator, VehiculeTypeExistsValidator vehiculeTypeExistsValidator)
        {
            RuleFor(c => c.Data.VehiculeTypeId).NotEmpty().WithMessage(ValidationMessage.NotEmpty("VehiculeTypeId"));
            RuleFor(c => c.Data.VehiculeTypeId).SetValidator(vehiculeTypeExistsValidator);
        }
    }
}
