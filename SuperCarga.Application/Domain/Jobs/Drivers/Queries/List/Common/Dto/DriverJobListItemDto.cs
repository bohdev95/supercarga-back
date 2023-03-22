using SuperCarga.Application.Domain.Customers.Driver.Dto;
using SuperCarga.Application.Domain.Location.Dto;

namespace SuperCarga.Application.Domain.Jobs.Drivers.Queries.List.Common.Dto
{
    public class DriverJobListItemDto
    {
        public Guid Id { get; set; }

        public DateTime Created { get; set; }

        public string Tittle { get; set; }

        public string VehiculeTypeName { get; set; }

        public decimal PricePerKm { get; set; }

        public DateTime PickupDate { get; set; }

        public decimal Distance { get; set; }

        public bool PrpoposalAlreadyAdded { get; set; }

        public bool AddedToFavorite { get; set; }

        public AddressDto Origin { get; set; }

        public AddressDto Destination { get; set; }

        public DriverCustomerDto Customer { get; set; }
    }
}
