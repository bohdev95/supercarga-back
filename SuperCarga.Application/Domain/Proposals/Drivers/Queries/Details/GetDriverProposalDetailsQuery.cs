using MediatR;
using SuperCarga.Application.Domain.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Proposals.Drivers.Queries.Details
{
    public class GetDriverProposalDetailsRequest
    {
        public Guid ProposalId { get; set; }
    }

    public class GetDriverProposalDetailsQuery : UserRequest<GetDriverProposalDetailsRequest, GetDriverProposalDetailsQueryResponse>
    {
    }
}
