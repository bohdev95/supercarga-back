using SuperCarga.Application.Domain.Common.Model;
using SuperCarga.Application.Domain.Costs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Contracts.Common.Models
{
    public class ContractAdditionalCost : AdditionalCost
    {
        public Guid ContractId { get; set; } 
    }
}
