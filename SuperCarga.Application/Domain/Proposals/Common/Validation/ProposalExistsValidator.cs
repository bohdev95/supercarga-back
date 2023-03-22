using FluentValidation;
using SuperCarga.Application.Domain.Proposals.Common.Abstraction;
using SuperCarga.Application.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Proposals.Common.Validation
{
    public class ProposalExistsValidator : AbstractValidator<Guid>
    {
        public ProposalExistsValidator(IProposalsService proposalsService)
        {
            RuleFor(x => x).Custom((contractId, ctx) =>
            {
                if (!proposalsService.ProposalExists(contractId))
                    ctx.AddFailure("proposalId", ValidationMessage.NotExist("Proposal"));
            });
        }
    }
}
