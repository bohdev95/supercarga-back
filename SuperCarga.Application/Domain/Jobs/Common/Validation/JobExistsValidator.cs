using FluentValidation;
using SuperCarga.Application.Domain.Jobs.Common.Abstraction;
using SuperCarga.Application.Validation;

namespace SuperCarga.Application.Domain.Jobs.Common.Validation
{
    public class JobExistsValidator : AbstractValidator<Guid>
    {
        public JobExistsValidator(IJobsService jobsService)
        {
            RuleFor(x => x).Custom((JobId, ctx) =>
            {
                if (!jobsService.JobExists(JobId))
                    ctx.AddFailure("JobId", ValidationMessage.NotExist("Job"));
            });
        }
    }
}
