using FluentValidation;
using SuperCarga.Application.Domain.Jobs.Common.Abstraction;
using SuperCarga.Application.Validation;

namespace SuperCarga.Application.Domain.Jobs.Common.Validation
{
    public abstract class JobStateValidator : AbstractValidator<Guid>
    {
        public JobStateValidator(IJobsService jobsService, string jobState)
        {
            RuleFor(x => x).Custom((JobId, ctx) =>
            {
                var actualJobState = jobsService.GetState(JobId);

                if (actualJobState != jobState)
                    ctx.AddFailure("JobId", ValidationMessage.InvalidState("Job"));
            });
        }
    }

}
