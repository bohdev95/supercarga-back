using SuperCarga.Application.Domain.Common.Model;
using SuperCarga.Application.Domain.Contracts.Common.Models;

namespace SuperCarga.Application.Domain.Finances.Model
{
    public class BalanceHold : Entity
    {
        public Guid FinanceId { get; set; }
        
        public decimal Value { get; set; }

        public Guid RelatedContractId { get; set; }

        public Contract RelatedContract { get; set; }
    }
}
