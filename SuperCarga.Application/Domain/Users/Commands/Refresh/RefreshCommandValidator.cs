using FluentValidation;
using SuperCarga.Application.Validation;

namespace SuperCarga.Application.Domain.Users.Commands.Refresh
{
    public class RefreshCommandValidator : AbstractValidator<RefreshCommand>
    {
        public RefreshCommandValidator()
        {
            RuleFor(c => c.AccesToken).NotEmpty().WithMessage(ValidationMessage.NotEmpty("AccesToken"));
            RuleFor(c => c.RefreshToken).NotEmpty().WithMessage(ValidationMessage.NotEmpty("RefreshToken"));
        }
    }
}
