using SuperCarga.Application.Domain.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Jobs.Drivers.Queries.List.Common.Dto
{
    public class DriverListJobRequest : ListRequestDto
    {
        public DateTime? CreatedFrom { get; set; }

        public DateTime? CreatedTo { get; set; }

        public DateTime? PickupFrom { get; set; }

        public DateTime? PickupTo { get; set; }

        public decimal? PricePerKmFrom { get; set; }

        public decimal? PricePerKmTo { get; set; }

        public decimal? DistanceFrom { get; set; }

        public decimal? DistanceTo { get; set; }

        public string? Search { get; set; }
    }
}
