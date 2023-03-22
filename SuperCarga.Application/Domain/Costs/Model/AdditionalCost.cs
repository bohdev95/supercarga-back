using SuperCarga.Application.Domain.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Costs.Model
{
    public class AdditionalCost : Entity
    { 
        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
