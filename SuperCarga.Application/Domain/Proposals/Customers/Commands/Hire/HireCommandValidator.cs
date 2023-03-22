using FluentValidation;
using SuperCarga.Application.Domain.Proposals.Common.Validation;
using SuperCarga.Application.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Proposals.Customers.Commands.Hire
{
    public class HireCommandValidator : AbstractValidator<HireCommand>
    {
        public HireCommandValidator(ProposalExistsValidator proposalExistsValidator)
        {
            RuleFor(c => c.Data.ProposalId).NotEmpty().WithMessage(ValidationMessage.NotEmpty("ProposalId"));
            RuleFor(c => c.Data.ProposalId).SetValidator(proposalExistsValidator);
        }
    }
}
