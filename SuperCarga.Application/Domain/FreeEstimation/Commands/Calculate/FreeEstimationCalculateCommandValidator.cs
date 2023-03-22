using FluentValidation;
using SuperCarga.Application.Domain.Common.Validation;
using SuperCarga.Application.Validation;

namespace SuperCarga.Application.Domain.FreeEstimation.Commands.Calculate
{
    public class FreeEstimationCalculateCommandValidator : AbstractValidator<FreeEstimationCalculateCommand>
    {
        public FreeEstimationCalculateCommandValidator(CargoDtoValidator cargoValidator)
        {
            RuleFor(c => c.Cargo).NotEmpty().WithMessage(ValidationMessage.NotEmpty("Cargo"));
            RuleFor(c => c.Cargo).SetValidator(cargoValidator);

            RuleFor(c => c.EstimatedDistance).NotEmpty().WithMessage(ValidationMessage.NotEmpty("EstimatedDistance"));
            RuleFor(c => c.RequireLoadingCrew).NotEmpty().WithMessage(ValidationMessage.NotEmpty("RequireLoadingCrew"));
            RuleFor(c => c.RequireUnloadingCrew).NotEmpty().WithMessage(ValidationMessage.NotEmpty("RequireUnloadingCrew"));
            RuleFor(c => c.Email).EmailAddress().WithMessage(ValidationMessage.NotCorrectFormat("Email"));
            RuleFor(c => c.CustomerName).NotEmpty().WithMessage(ValidationMessage.NotEmpty("CustomerName"));
        }
    }
}
