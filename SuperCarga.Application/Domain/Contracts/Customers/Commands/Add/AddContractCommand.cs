using MediatR;
using SuperCarga.Application.Domain.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Contracts.Customers.Commands.Add
{
    public class AddContractRequest
    {
        public Guid ProposalId { get; set; }

        //public decimal PayUpfront { get; set; } TODO payments
    }

    public class AddContractCommand : UserRequest<AddContractRequest, AddContractCommandResponse>
    {
    }
}
