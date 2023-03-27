using SuperCarga.Application.Domain.Common.Model;

namespace SuperCarga.Application.Domain.Finances.Model
{
    public class FinanceHistory : Entity
    {
        public Guid FinanceId { get; set; }

        public string Operation { get; set; }

        public decimal BalanceBefore { get; set; }

        public decimal BalanceAfter { get; set; }

        public decimal OperationValue { get; set; }

        public Guid? FromUserId { get; set; }
        
        public Guid? ToUserId { get; set; }

        public Guid? RelatedContractId { get; set; }
    }
}
