using SuperCarga.Application.Domain.Contracts.Common.Abstraction;
using SuperCarga.Application.Domain.Contracts.Common.Models;

namespace SuperCarga.Application.Domain.Contracts.Common.Validation
{
    public class ContractDeliveredValidator : ContractStateValidator
    {
        public ContractDeliveredValidator(IContractsService contractsService) : base(contractsService, ContractState.Delivered)
        {
        }
    }
}
