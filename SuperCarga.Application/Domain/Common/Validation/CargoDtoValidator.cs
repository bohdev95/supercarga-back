using FluentValidation;
using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Validation;

namespace SuperCarga.Application.Domain.Common.Validation
{
    public class CargoDtoValidator : AbstractValidator<CargoDimensionsDto>
    {
        public CargoDtoValidator()
        {
            RuleFor(c => c.Weight).GreaterThan(0).WithMessage(ValidationMessage.GreaterThan("Weight", 0));
            RuleFor(c => c.Width).GreaterThan(0).WithMessage(ValidationMessage.GreaterThan("CargoWidth", 0));
            RuleFor(c => c.Height).GreaterThan(0).WithMessage(ValidationMessage.GreaterThan("CargoHeight", 0));
            RuleFor(c => c.Lenght).GreaterThan(0).WithMessage(ValidationMessage.GreaterThan("CargoLenght", 0));
        }
    }
}
