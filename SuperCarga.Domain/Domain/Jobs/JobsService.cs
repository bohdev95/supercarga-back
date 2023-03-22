using SuperCarga.Application.Domain.Jobs.Common.Abstraction;
using SuperCarga.Persistence.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Domain.Domain.Jobs
{
    public class JobsService : IJobsService
    {
        private readonly SuperCargaContext ctx;

        public JobsService(SuperCargaContext ctx)
        {
            this.ctx = ctx;
        }

        public string GetState(Guid id) => ctx.Jobs.Where(x => x.Id == id).Select(x => x.State).FirstOrDefault();

        public bool JobExists(Guid id) => ctx.Jobs.Where(x => x.Id == id).Any();
    }
}
