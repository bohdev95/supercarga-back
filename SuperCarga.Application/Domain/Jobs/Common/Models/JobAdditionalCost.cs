using SuperCarga.Application.Domain.Costs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Jobs.Common.Models
{
    public class JobAdditionalCost : AdditionalCost
    {
        public Guid JobId { get; set; }
    }
}
