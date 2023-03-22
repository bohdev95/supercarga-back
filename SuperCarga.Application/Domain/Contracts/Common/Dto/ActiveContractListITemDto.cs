using SuperCarga.Application.Domain.Location.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Contracts.Common.Dto
{
    public class ActiveContractListITemDto
    {
        public Guid Id { get; set; }

        public DateTime Created { get; set; }

        public string Tittle { get; set; }

        public AddressDto Origin { get; set; }

        public AddressDto Destination { get; set; }

        public string PaymentState { get; set; } //TODO

        public string State { get; set; }

        public DateTime StateChanged { get; set; }

        public bool IsInDispute { get; set; }
    }
}
