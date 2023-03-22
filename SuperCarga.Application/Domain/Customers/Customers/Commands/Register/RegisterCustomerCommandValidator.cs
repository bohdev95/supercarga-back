using FluentValidation;
using SuperCarga.Application.Domain.Users.Validation;
using SuperCarga.Application.Validation;

namespace SuperCarga.Application.Domain.Customers.Customers.Commands.Register
{
    public class RegisterCustomerCommandValidator : AbstractValidator<RegisterCustomerCommand>
    {
        public RegisterCustomerCommandValidator(UserAlreadyExistsValidator userAleadyExistsValidator)
        {
            RuleFor(c => c.Email).NotEmpty().WithMessage(ValidationMessage.NotEmpty("Email"));
            RuleFor(c => c.Email).EmailAddress().WithMessage(ValidationMessage.NotCorrectFormat("Email"));
            RuleFor(c => c.Email).SetValidator(userAleadyExistsValidator);

            RuleFor(c => c.Password).NotEmpty().WithMessage(ValidationMessage.NotEmpty("Password"));
            RuleFor(c => c).Must(c => c.Password == c.ConfirmPassword).WithName("Password").WithMessage(ValidationMessage.NotMatch("Password", "ConfirmPassword"));

            //RuleFor(c => c.Data.Cargo.Image).SetValidator(imageDtoValidator).When(image => image != null);

            //TODO
        }
    }
}
