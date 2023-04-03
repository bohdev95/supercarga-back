using SuperCarga.Application.Domain.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Proposals.Customers.Queries.List.Common.Dto
{
    public class CustomerListProposalsRequest : ListRequestDto
    {
        public Guid? JobId { get; set; }

        public DateTime? CreatedFrom { get; set; }

        public DateTime? CreatedTo { get; set; }

        public string? State { get; set; }

        public decimal? PricePerKmFrom { get; set; }

        public decimal? PricePerKmTo { get; set; }
    }
}
