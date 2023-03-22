using FluentValidation;
using SuperCarga.Application.Domain.Common.Validation;

namespace SuperCarga.Application.Domain.VehiculeTypes.Commands.CheckType
{
    public class CheckVehiculeTypeCommandValidator : AbstractValidator<CheckVehiculeTypeCommand>
    {
        public CheckVehiculeTypeCommandValidator(CargoDtoValidator cargoValidator)
        {
            RuleFor(c => c).SetValidator(cargoValidator);
        }
    }
}
