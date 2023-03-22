using FluentValidation;
using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Validation;

namespace SuperCarga.Application.Domain.Common.Validation
{
    public class ImageDtoValidator : AbstractValidator<ImageDto>
    {
        public ImageDtoValidator()
        {
            RuleFor(c => c.Content).NotEmpty().WithMessage(ValidationMessage.NotEmpty("Content"));
            RuleFor(c => c.Extension).Must(x =>
            {
                var normalized = x.ToLower().Trim();

                return normalized == ".jpg" || normalized == ".jpeg" || normalized == ".png"; //TODO 
            })
            .WithMessage(ValidationMessage.NotCorrectFormat("Extension"));
        }
    }
}
