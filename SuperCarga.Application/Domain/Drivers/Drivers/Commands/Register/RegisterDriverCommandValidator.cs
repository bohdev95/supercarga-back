using FluentValidation;
using SuperCarga.Application.Domain.Users.Validation;
using SuperCarga.Application.Domain.VehiculeTypes.Validation;
using SuperCarga.Application.Validation;

namespace SuperCarga.Application.Domain.Drivers.Drivers.Commands.Register
{
    public class RegisterDriverCommandValidator : AbstractValidator<RegisterDriverCommand>
    {
        public RegisterDriverCommandValidator(UserAlreadyExistsValidator userAleadyExistsValidator, VehiculeTypeExistsValidator vehiculeTypeExistsValidator)
        {
            RuleFor(c => c.Email).NotEmpty().WithMessage(ValidationMessage.NotEmpty("Email"));
            RuleFor(c => c.Email).EmailAddress().WithMessage(ValidationMessage.NotCorrectFormat("Email"));
            RuleFor(c => c.Email).SetValidator(userAleadyExistsValidator);

            RuleFor(c => c.Password).NotEmpty().WithMessage(ValidationMessage.NotEmpty("Password"));
            RuleFor(c => c).Must(c => c.Password == c.ConfirmPassword).WithName("Password").WithMessage(ValidationMessage.NotMatch("Password", "ConfirmPassword"));

            RuleFor(c => c.VehiculeTypeId).NotEmpty().WithMessage(ValidationMessage.NotEmpty("VehiculeTypeId"));
            RuleFor(c => c.VehiculeTypeId).SetValidator(vehiculeTypeExistsValidator);
        }
    }
}
