using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Common.Model
{
    public abstract class Entity
    {
        public Guid Id { get; set; }

        public DateTime Created { get; set; }
    }
}
