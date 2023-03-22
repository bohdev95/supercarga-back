using MediatR;
using SuperCarga.Application.Domain.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Proposals.Drivers.Commands.Add
{
    public class AddProposalRequest
    {
        public Guid JobId { get; set; }

        public decimal PricePerKm { get; set; }
    }

    public class AddProposalCommand : UserRequest<AddProposalRequest, AddProposalCommandResponse>
    {
    }
}
