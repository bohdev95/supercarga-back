using SuperCarga.Application.Domain.Costs.Dto;
using SuperCarga.Application.Domain.Proposals.Common.Abstraction;
using SuperCarga.Persistence.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Domain.Domain.Proposals
{
    public class ProposalsService : IProposalsService
    {
        protected readonly SuperCargaContext ctx;

        public ProposalsService(SuperCargaContext ctx)
        {
            this.ctx = ctx;
        }

        public string GetState(Guid id) => ctx.Proposals.Where(x => x.Id == id).Select(x => x.State).FirstOrDefault();

        public bool ProposalExists(Guid id) => ctx.Proposals.Where(x => x.Id == id).Any();
    }
}
