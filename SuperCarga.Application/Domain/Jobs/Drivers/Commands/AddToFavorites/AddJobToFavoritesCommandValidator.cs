using FluentValidation;
using SuperCarga.Application.Domain.Jobs.Common.Validation;
using SuperCarga.Application.Validation;

namespace SuperCarga.Application.Domain.Jobs.Drivers.Commands.AddToFavorites
{
    public class AddJobToFavoritesCommandValidator : AbstractValidator<AddJobToFavoritesCommand>
    {
        public AddJobToFavoritesCommandValidator(JobExistsValidator jobNotExistsValidator)
        {
            RuleFor(c => c.Data.JobId).NotEmpty().WithMessage(ValidationMessage.NotEmpty("JobId"));
            RuleFor(c => c.Data.JobId).SetValidator(jobNotExistsValidator);
        }
    }
}
