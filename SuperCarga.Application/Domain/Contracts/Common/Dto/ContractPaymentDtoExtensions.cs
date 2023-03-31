using SuperCarga.Application.Domain.Contracts.Common.Models;

namespace SuperCarga.Application.Domain.Contracts.Common.Dto
{
    public static class ContractPaymentDtoExtensions
    {
        public static List<ContractPaymentDto> GetContractPayment(this Contract contract) => contract.Payments.Select(x => new ContractPaymentDto
        {
            Date = x.Created,
            Value = x.OperationValue,
            Operation = x.Operation
        })
        .ToList();
    }
}
