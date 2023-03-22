using SuperCarga.Application.Domain.Contracts.Common.Abstraction;
using SuperCarga.Application.Domain.Contracts.Common.Models;

namespace SuperCarga.Application.Domain.Contracts.Common.Validation
{
    public class ContractDeliveryConfirmedValidator : ContractStateValidator
    {
        public ContractDeliveryConfirmedValidator(IContractsService contractsService) : base(contractsService, ContractState.DeliveredConfirmed)
        {
        }
    }
}
