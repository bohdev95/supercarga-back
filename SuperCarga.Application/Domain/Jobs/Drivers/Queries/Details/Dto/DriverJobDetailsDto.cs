using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Customers.Driver.Dto;
using SuperCarga.Application.Domain.Location.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Jobs.Drivers.Queries.Details.Dto
{
    public class DriverJobsDetailsDto
    {
        public Guid Id { get; set; }

        public DateTime Created { get; set; }

        public string VehiculeTypeName { get; set; }

        public bool RequireLoadingCrew { get; set; }

        public bool RequireUnloadingCrew { get; set; }

        public bool PrpoposalAlreadyAdded { get; set; }

        public string Description { get; set; }

        public string Tittle { get; set; }

        public int Distance { get; set; }

        public decimal PricePerKm { get; set; }

        public DateTime PickupDate { get; set; }

        public DateTime DeliveryDate { get; set; }

        public CargoDto Cargo { get; set; }

        public AddressDto Origin { get; set; }

        public AddressDto Destination { get; set; }

        public DriverCustomerDto Customer { get; set; }

        public string State { get; set; }
    }
}
