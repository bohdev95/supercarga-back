using SuperCarga.Application.Domain.Contracts.Common.Abstraction;
using SuperCarga.Application.Domain.Contracts.Common.Models;

namespace SuperCarga.Application.Domain.Contracts.Common.Validation
{
    public class ContractCreatedValidator : ContractStateValidator
    {
        public ContractCreatedValidator(IContractsService contractsService) : base(contractsService, ContractState.Created)
        {
        }
    }
}
