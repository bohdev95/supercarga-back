using MediatR;
using SuperCarga.Application.Domain.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Proposals.Customers.Commands.Hire
{
    public class HireRequest
    {
        public Guid ProposalId { get; set; }
    }

    public class HireCommand : UserRequest<HireRequest, HireCommandResponse>
    {
    }
}
