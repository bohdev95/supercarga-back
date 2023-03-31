using SuperCarga.Application.Domain.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Finances.Model
{
    public class Payment : Entity
    {
        public string Operation { get; set; }

        public decimal OperationValue { get; set; }

        public decimal? FromUserBalanceBefore { get; set; }

        public decimal? FromUserBalanceAfter { get; set; }        

        public Guid? FromUserId { get; set; }

        public decimal? ToUserBalanceBefore { get; set; }

        public decimal? ToUserBalanceAfter { get; set; }

        public Guid? ToUserId { get; set; }

        public Guid? RelatedContractId { get; set; }
    }
}
