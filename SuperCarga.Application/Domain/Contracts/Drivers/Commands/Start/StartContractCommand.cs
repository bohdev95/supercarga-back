using MediatR;
using SuperCarga.Application.Domain.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Contracts.Drivers.Commands.Start
{
    public class StartContractRequest
    {
        public Guid ProposalId { get; set; }
    }

    public class StartContractCommand : UserRequest<StartContractRequest, StartContractCommandResponse>
    {
    }
}
