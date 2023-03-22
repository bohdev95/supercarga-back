using SuperCarga.Application.Domain.Customers.Driver.Dto;
using SuperCarga.Application.Domain.Location.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Proposals.Drivers.Queries.List.Common.Dto
{
    public class DriverProposalListItemDto
    {
        public Guid Id { get; set; }

        public DateTime Created { get; set; }

        public Guid JobId { get; set; }

        public decimal PricePerKm { get; set; }

        public string JobTittle { get; set; }

        public string State { get; set; }

        public DateTime PickupDate { get; set; }

        public AddressDto Origin { get; set; }

        public AddressDto Destination { get; set; }

        public DriverCustomerDto Customer { get; set; }
    }
}
