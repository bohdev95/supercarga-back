using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Proposals.Common.Abstraction
{
    public interface IProposalsService
    {
        bool ProposalExists(Guid id);

        string GetState(Guid id);
    }
}
