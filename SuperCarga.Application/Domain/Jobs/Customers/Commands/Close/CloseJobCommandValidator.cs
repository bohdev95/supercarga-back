using FluentValidation;
using SuperCarga.Application.Domain.Jobs.Common.Validation;
using SuperCarga.Application.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Jobs.Customers.Commands.Close
{
    public class CloseJobCommandValidator : AbstractValidator<CloseJobCommand>
    {
        public CloseJobCommandValidator(JobExistsValidator jobExistsValidator, JobActiveValidator jobActiveValidator)
        {
            RuleFor(c => c.Data.JobId).NotEmpty().WithMessage(ValidationMessage.NotEmpty("JobId"));
            RuleFor(c => c.Data.JobId).SetValidator(jobExistsValidator);
            RuleFor(c => c.Data.JobId).SetValidator(jobActiveValidator);
        }
    }
}
