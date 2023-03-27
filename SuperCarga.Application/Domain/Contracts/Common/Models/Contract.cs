using SuperCarga.Application.Domain.Common.Model;
using SuperCarga.Application.Domain.Customers.Common.Models;
using SuperCarga.Application.Domain.Drivers.Common.Models;
using SuperCarga.Application.Domain.Jobs.Common.Models;
using SuperCarga.Application.Domain.Proposals.Common.Models;

namespace SuperCarga.Application.Domain.Contracts.Common.Models
{
    public class Contract : Entity
    {
        public Guid ProposalId { get; set; }

        public Proposal Proposal { get; set; }

        public Guid JobId { get; set; }

        public Job Job { get; set; }

        public Guid DriverId { get; set; }

        public Driver Driver { get; set; }

        public Guid CustomerId { get; set; }

        public Customer Customer { get; set; }

        public string State { get; set; }
        
        public string PaymentState { get; set; } 

        #region costs

        public decimal PricePerKm { get; set; }

        public decimal PricePerDistance { get; set; }

        public decimal Price { get; set; }

        public decimal ServiceFee { get; set; }

        public decimal TotalPrice { get; set; }

        public List<ContractAdditionalCost> Additions { get; set; }

        #endregion

        public List<ContractHistory> History { get; set; }

        public string? PickUpCargoImagePath { get; set; }

        public string? PickUpProofImagePath { get; set; }

        public string? DeliveryCargoImagePath { get; set; }

        public string? DeliveryProofImagePath { get; set; }

        #region rating

        public decimal? Rating { get; set; }

        public string? RatingComment { get; set; }

        #endregion
    }
}
