using SuperCarga.Application.Domain.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Costs.Model
{
    public static class CostType
    {
        public const string ServiceFee = "SERVICE_FEE";
        public const string Loading = "LOADING";
        public const string Unloading = "UNLOADING";

        public static bool Contains(string type)
        {
            var normalizedType = type.ToUpper().Trim();

            return normalizedType == ServiceFee
                || normalizedType == Loading
                || normalizedType == Unloading;
        }
    }

    public class Cost : Entity
    {
        public string Type { get; set; }

        public decimal Value { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime? ToDate { get; set; }
    }
}
