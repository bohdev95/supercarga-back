using SuperCarga.Application.Domain.Common.Model;
using SuperCarga.Application.Domain.Contracts.Common.Models;
using SuperCarga.Application.Domain.Customers.Common.Models;
using SuperCarga.Application.Domain.Drivers.Common.Models;
using SuperCarga.Application.Domain.Proposals.Common.Models;
using SuperCarga.Application.Domain.VehiculeTypes.Models;

namespace SuperCarga.Application.Domain.Jobs.Common.Models
{
    public class Job : Entity
    {
        public Guid CustomerId { get; set; }

        public Customer Customer { get; set; }

        public List<Contract> Contracts { get; set; }

        public List<Proposal> Proposals { get; set; }

        #region cargo

        public decimal CargoWeight { get; set; }

        public decimal CargoWidth { get; set; }

        public decimal CargoHeight { get; set; }

        public decimal CargoLenght { get; set; }

        public string? CargoImagePath { get; set; }

        #endregion

        #region origin

        public string OriginCity { get; set; }

        public string OriginStreet { get; set; }

        public string OriginPostCode { get; set; }

        #endregion

        #region destination

        public string DestinationCity { get; set; }

        public string DestinationStreet { get; set; }

        public string DestinationPostCode { get; set; }

        #endregion

        public bool RequireLoadingCrew { get; set; }

        public bool RequireUnloadingCrew { get; set; }

        public string Description { get; set; }

        public string Tittle { get; set; }

        public int Distance { get; set; }

        public string State { get; set; }

        public Guid VehiculeTypeId { get; set; }

        public VehiculeType VehiculeType { get; set; }

        public List<Driver> AddedToFavoriteBy { get; set; }

        public DateTime PickupDate { get; set; }

        public DateTime DeliveryDate { get; set; }

        #region costs

        public decimal PricePerKm { get; set; }

        public decimal PricePerDistance { get; set; }

        public decimal Price { get; set; }

        public decimal ServiceFee { get; set; }

        public decimal TotalPrice { get; set; }

        public List<JobAdditionalCost> Additions { get; set; }

        #endregion
    }
}
