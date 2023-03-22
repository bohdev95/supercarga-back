using SuperCarga.Application.Domain.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Finances.Model
{
    public class Finance : Entity
    {
        public Guid UserId { get; set; }

        public decimal Balance { get; set; }
    }
}
