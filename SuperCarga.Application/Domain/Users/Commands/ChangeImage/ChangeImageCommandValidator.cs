using FluentValidation;
using SuperCarga.Application.Domain.Common.Validation;
using SuperCarga.Application.Validation;

namespace SuperCarga.Application.Domain.Users.Commands.ChangeImage
{
    public class ChangeImageCommandValidator : AbstractValidator<ChangeImageCommand>
    {
        public ChangeImageCommandValidator(ImageDtoValidator imageValidator)
        {
            RuleFor(c => c.Data.Image).NotEmpty().WithMessage(ValidationMessage.NotEmpty("Image"));
            RuleFor(c => c.Data.Image).SetValidator(imageValidator);
        }
    }
}
