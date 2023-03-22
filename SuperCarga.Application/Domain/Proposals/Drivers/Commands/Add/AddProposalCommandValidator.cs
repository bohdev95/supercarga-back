using FluentValidation;
using SuperCarga.Application.Domain.Jobs.Common.Validation;
using SuperCarga.Application.Validation;

namespace SuperCarga.Application.Domain.Proposals.Drivers.Commands.Add
{
    public class AddProposalCommandValidator : AbstractValidator<AddProposalCommand>
    {
        public AddProposalCommandValidator(JobExistsValidator jobExistsValidator, JobActiveValidator jobActiveValidator)
        {
            RuleFor(c => c.Data.JobId).NotEmpty().WithMessage(ValidationMessage.NotEmpty("JobId"));
            RuleFor(c => c.Data.JobId).SetValidator(jobExistsValidator);
            RuleFor(c => c.Data.JobId).SetValidator(jobActiveValidator);
        }
    }
}
