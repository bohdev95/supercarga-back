using SuperCarga.Application.Domain.Finances.Queries.GetBalance.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Finances.Queries.GetBalance
{
    public class GetBalanceQueryResponse
    {
        public BalanceDto Balance { get; set; }
    }
}
