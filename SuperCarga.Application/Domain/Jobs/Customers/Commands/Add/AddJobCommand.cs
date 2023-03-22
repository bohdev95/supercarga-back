using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Common.Model;
using SuperCarga.Application.Domain.Jobs.Customers.Commands.Add.Dto;
using SuperCarga.Application.Domain.Location.Dto;

namespace SuperCarga.Application.Domain.Jobs.Customers.Commands.Add
{
    public class AddJobRequest
    {
        public string Tittle { get; set; }

        public string Description { get; set; }

        public DateTime PickupDate { get; set; }

        public DateTime DeliveryDate { get; set; }

        public AddJobCargoDto Cargo { get; set; }

        public Guid VehiculeTypeId { get; set; }

        public AddressDto Origin { get; set; }

        public AddressDto Destination { get; set; }

        public int Distance { get; set; }

        public bool RequireLoadingCrew { get; set; }

        public bool RequireUnloadingCrew { get; set; }

        public decimal PricePerKm { get; set; }   
    }

    public class AddJobCommand : UserRequest<AddJobRequest, AddJobCommandResponse>
    {
    }
}
