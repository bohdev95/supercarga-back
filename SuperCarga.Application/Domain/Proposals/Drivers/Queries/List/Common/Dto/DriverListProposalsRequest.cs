using SuperCarga.Application.Domain.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Proposals.Drivers.Queries.List.Common.Dto
{
    public class DriverListProposalsRequest : ListRequestDto
    {
        public Guid? JobId { get; set; }

        public DateTime? CreatedFrom { get; set; }

        public DateTime? CreatedTo { get; set; }

        public DateTime? PickupFrom { get; set; }

        public DateTime? PickupTo { get; set; }

        public string? State { get; set; }

        public decimal? PricePerKmFrom { get; set; }

        public decimal? PricePerKmTo { get; set; }
    }
}
