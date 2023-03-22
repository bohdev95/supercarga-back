using FluentValidation;
using SuperCarga.Application.Domain.Location.Validation;
using SuperCarga.Application.Validation;

namespace SuperCarga.Application.Domain.Location.Commands.CheckDistance
{
    public class CheckDistanceCommandValidator : AbstractValidator<CheckDistanceCommand>
    {
        public CheckDistanceCommandValidator(AddressDtoValidator adressValidator)
        {
            RuleFor(c => c.Origin).NotEmpty().WithMessage(ValidationMessage.NotEmpty("Origin"));
            RuleFor(c => c.Origin).SetValidator(adressValidator);

            RuleFor(c => c.Destination).NotEmpty().WithMessage(ValidationMessage.NotEmpty("Destination"));
            RuleFor(c => c.Destination).SetValidator(adressValidator);
        }
    }
}
