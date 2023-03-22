using MediatR;
using SuperCarga.Application.Domain.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Contracts.Customers.Commands.Finalize
{
    public class CustomerFinalizeRequest
    {
        public Guid ContractId { get; set; }

        public decimal Rating { get; set; }

        public string RatingComment { get; set; }
    }

    public class CustomerFinalizeCommand : UserRequest<CustomerFinalizeRequest, CustomerFinalizeCommandResponse>
    {
    }
}
