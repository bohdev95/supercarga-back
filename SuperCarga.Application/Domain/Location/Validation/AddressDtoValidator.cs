using FluentValidation;
using SuperCarga.Application.Domain.Location.Dto;
using SuperCarga.Application.Validation;

namespace SuperCarga.Application.Domain.Location.Validation
{
    public class AddressDtoValidator : AbstractValidator<AddressDto>
    {
        public AddressDtoValidator()
        {
            RuleFor(c => c.City).NotEmpty().WithMessage(ValidationMessage.NotEmpty("City"));
            RuleFor(c => c.Street).NotEmpty().WithMessage(ValidationMessage.NotEmpty("Street"));
            RuleFor(c => c.PostCode).NotEmpty().WithMessage(ValidationMessage.NotEmpty("PostCode"));
        }
    }
}
