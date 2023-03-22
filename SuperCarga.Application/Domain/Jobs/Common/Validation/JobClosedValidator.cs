using SuperCarga.Application.Domain.Jobs.Common.Abstraction;
using SuperCarga.Application.Domain.Jobs.Common.Models;

namespace SuperCarga.Application.Domain.Jobs.Common.Validation
{
    public class JobClosedValidator : JobStateValidator
    {
        public JobClosedValidator(IJobsService jobsService) : base(jobsService, JobState.Closed)
        {
        }
    }

}
