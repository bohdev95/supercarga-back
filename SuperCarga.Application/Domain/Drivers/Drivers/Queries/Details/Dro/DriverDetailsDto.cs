using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Drivers.Drivers.Queries.Details.Dro
{
    public class DriverDetailsDto
    {
        public Guid Id { get; set; }

        public DateTime Created { get; set; }

        public decimal? Rating { get; set; }

        public int RatedContracts { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string ImagePath { get; set; }

        public string? DriverLicensePath { get; set; }

        public string VehiculeTypeName { get; set; }

        public string VerifivationState { get; set; }

        public decimal Balance { get; set; }
    }
}
