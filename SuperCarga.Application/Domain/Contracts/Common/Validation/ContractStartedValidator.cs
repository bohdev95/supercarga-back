using SuperCarga.Application.Domain.Contracts.Common.Abstraction;
using SuperCarga.Application.Domain.Contracts.Common.Models;

namespace SuperCarga.Application.Domain.Contracts.Common.Validation
{
    public class ContractStartedValidator : ContractStateValidator
    {
        public ContractStartedValidator(IContractsService contractsService) : base(contractsService, ContractState.Started)
        {
        }
    }
}
