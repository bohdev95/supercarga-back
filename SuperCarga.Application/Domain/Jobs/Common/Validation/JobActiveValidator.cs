using SuperCarga.Application.Domain.Jobs.Common.Abstraction;
using SuperCarga.Application.Domain.Jobs.Common.Models;

namespace SuperCarga.Application.Domain.Jobs.Common.Validation
{
    public class JobActiveValidator : JobStateValidator
    {
        public JobActiveValidator(IJobsService jobsService) : base(jobsService, JobState.Active)
        {
        }
    }

}
