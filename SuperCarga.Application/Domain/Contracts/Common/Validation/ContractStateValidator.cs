using FluentValidation;
using SuperCarga.Application.Domain.Contracts.Common.Abstraction;
using SuperCarga.Application.Domain.Proposals.Common.Abstraction;
using SuperCarga.Application.Validation;

namespace SuperCarga.Application.Domain.Contracts.Common.Validation
{
    public abstract class ContractStateValidator : AbstractValidator<Guid>
    {
        public ContractStateValidator(IContractsService contractsService, string contractState)
        {
            RuleFor(x => x).Custom((contractId, ctx) =>
            {
                var actualContractState = contractsService.GetState(contractId);

                if (actualContractState != contractState)
                    ctx.AddFailure("contractId", ValidationMessage.InvalidState("Contract"));
            });
        }
    }
}
