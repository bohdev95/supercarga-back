using FluentValidation;
using SuperCarga.Application.Validation;

namespace SuperCarga.Application.Domain.Users.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(c => c.Email).EmailAddress().WithMessage(ValidationMessage.NotCorrectFormat("Email"));
            RuleFor(c => c.Password).NotEmpty().WithMessage(ValidationMessage.NotEmpty("Password"));
        }
    }
}
