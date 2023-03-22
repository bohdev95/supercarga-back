using FluentValidation;
using SuperCarga.Application.Domain.Contracts.Common.Abstraction;
using SuperCarga.Application.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Contracts.Common.Validation
{
    public class ContractExistsValidator : AbstractValidator<Guid>
    {
        public ContractExistsValidator(IContractsService contractsService)
        {
            RuleFor(x => x).Custom((contractId, ctx) =>
            {
                if (!contractsService.ContractExists(contractId))
                    ctx.AddFailure("contractId", ValidationMessage.NotExist("Contract"));
            });
        }
    }
}
