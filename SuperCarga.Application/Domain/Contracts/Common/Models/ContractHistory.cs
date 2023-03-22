using SuperCarga.Application.Domain.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Contracts.Common.Models
{
    public class ContractHistory : Entity
    {
        public Guid ContractId { get; set; }

        public string State { get; set; }
    }
}
