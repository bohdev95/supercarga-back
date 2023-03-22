using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Jobs.Common.Abstraction
{
    public interface IJobsService
    {
        bool JobExists(Guid id);

        string GetState(Guid id);
    }
}
