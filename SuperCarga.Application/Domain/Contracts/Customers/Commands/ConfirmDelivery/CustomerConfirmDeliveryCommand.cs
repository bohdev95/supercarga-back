using MediatR;
using SuperCarga.Application.Domain.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Contracts.Customers.Commands.ConfirmDelivery
{
    public class CustomerConfirmDeliveryRequest 
    {
        public Guid ContractId { get; set; }
    }

    public class CustomerConfirmDeliveryCommand : UserRequest<CustomerConfirmDeliveryRequest, CustomerConfirmDeliveryCommandResponse>
    {
    }
}
