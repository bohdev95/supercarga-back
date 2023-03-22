using FluentValidation;
using SuperCarga.Application.Domain.Common.Validation;
using SuperCarga.Application.Validation;

namespace SuperCarga.Application.Domain.Customers.Customers.Commands.UploadIdDocument
{
    public class UploadIdDocumentCommandValidator : AbstractValidator<UploadIdDocumentCommand>
    {
        public UploadIdDocumentCommandValidator(ImageDtoValidator imageValidator)
        {
            RuleFor(c => c.Data.IdDocument).NotEmpty().WithMessage(ValidationMessage.NotEmpty("IdDocument"));
            RuleFor(c => c.Data.IdDocument).SetValidator(imageValidator);
        }
    }
}
