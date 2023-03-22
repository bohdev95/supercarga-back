using FluentValidation;
using SuperCarga.Application.Domain.Common.Validation;
using SuperCarga.Application.Validation;

namespace SuperCarga.Application.Domain.Drivers.Drivers.Commands.UploadDrivingLicense
{
    public class UploadDrivingLicenseCommandValidator : AbstractValidator<UploadDrivingLicenseCommand>
    {
        public UploadDrivingLicenseCommandValidator(ImageDtoValidator imageValidator)
        {
            RuleFor(c => c.Data.DrivingLicense).NotEmpty().WithMessage(ValidationMessage.NotEmpty("DrivingLicense"));
            RuleFor(c => c.Data.DrivingLicense).SetValidator(imageValidator);
        }
    }
}
