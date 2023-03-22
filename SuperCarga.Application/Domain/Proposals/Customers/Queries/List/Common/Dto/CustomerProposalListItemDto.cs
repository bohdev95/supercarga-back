using SuperCarga.Application.Domain.Drivers.Customers.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Proposals.Customers.Queries.List.Common.Dto
{
    public class CustomerProposalListItemDto
    {
        public Guid Id { get; set; }

        public DateTime Created { get; set; }

        public Guid JobId { get; set; }

        public decimal PricePerKm { get; set; }

        public bool AddedToFavorite { get; set; }

        public string State { get; set; }

        public CustomerDriverDto Driver { get; set; }
    }
}
