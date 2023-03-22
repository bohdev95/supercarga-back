using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Location.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Jobs.Common.Queries.details.Dto
{
    public class JobDetailsDto
    {
        public Guid Id { get; set; }

        public DateTime Created { get; set; }

        public Guid CustomerId { get; set; }

        public string VehiculeType { get; set; }

        public CargoDimensionsDto Cargo { get; set; }

        public AddressDto Origin { get; set; }

        public AddressDto Destination { get; set; }

        public int Proposals { get; set; }

        public bool RequireLoadingCrew { get; set; }

        public bool RequireUnloadingCrew { get; set; }

        public string AdditionsalInformations { get; set; }

        public string Tittle { get; set; }

        public decimal Distance { get; set; }

        public bool RequireDocuments { get; set; }

        public decimal Price { get; set; }
    }
}
