using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Location.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Contracts.Common.Dto
{
    public class ContractDetailsDto
    {
        public Guid Id { get; set; }

        public DateTime Created { get; set; }

        public decimal? Rating { get; set; }

        public string VehiculeTypeName { get; set; }

        public string Description { get; set; }

        public string Tittle { get; set; }

        public int Distance { get; set; }

        public decimal PricePerKm { get; set; }

        public DateTime PickupDate { get; set; }

        public DateTime DeliveryDate { get; set; }

        public CargoDimensionsDto Cargo { get; set; }

        public AddressDto Origin { get; set; }

        public AddressDto Destination { get; set; }

        public string PaymentState { get; set; } //TODO

        public string State { get; set; }

        public DateTime StateChanged { get; set; }

        public bool IsInDispute { get; set; }

        public decimal Price { get; set; }

        public string PickUpCargoImagePath { get; set; }

        public string PickUpProofImagePath { get; set; }

        public string DeliveryCargoImagePath { get; set; }

        public string DeliveryProofImagePath { get; set; }
    }
}
