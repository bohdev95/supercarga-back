using SuperCarga.Application.Domain.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.VehiculeTypes.Models
{
    public class VehiculeType : Entity
    {
        public string Name { get; set; }

        public decimal PricePerKm { get; set; }

        public decimal MaxCargoWeight { get; set; }

        public decimal MaxCargoLenght { get; set; }

        public decimal MaxCargoWidth { get; set; }

        public decimal MaxCargoHeight { get; set; }

        public bool RequireDocuments { get; set; }
    }
}
