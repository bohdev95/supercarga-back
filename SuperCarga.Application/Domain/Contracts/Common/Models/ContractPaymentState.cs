using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Contracts.Common.Models
{
    public static class ContractPaymentState
    {
        public const string OnDeliveryConfirmation = "On delivery confirmation";
        public const string PrepaymentReceived = "Prepayment received";
        public const string PaidFull = "Paid full";
    }
}
