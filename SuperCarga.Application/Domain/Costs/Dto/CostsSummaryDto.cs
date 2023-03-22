using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Costs.Dto
{
    public class CostsSummaryDto 
    {
        public decimal PricePerKm { get; set; }

        public decimal PricePerDistance { get; set; }

        public List<AdditionCostDto> Additions { get; set; }

        public decimal Price { get; set; }

        public decimal ServiceFee { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
