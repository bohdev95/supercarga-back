using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Finances.Queries.GetBalance.Dto
{
    public class BalanceDto
    {
        public decimal Value { get; set; }

        public List<BalanceHoldDto> Holds { get; set; }
    }
}
