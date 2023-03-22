using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Common.Model;
using SuperCarga.Application.Domain.Proposals.Customers.Queries.List.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Proposals.Customers.Queries.List.Favorites
{
    public class CustomerListFavoritesProposalsRequest : ListRequestDto
    {
        public Guid? JobId { get; set; }
    }

    public class CustomerListFavoritesProposalsQuery : UserRequest<CustomerListFavoritesProposalsRequest, ListResponseDto<CustomerProposalListItemDto>>
    {
    }
}
