using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Proposals.Customers.Commands.AddToFavorites;
using SuperCarga.Application.Domain.Proposals.Customers.Commands.RemoveFromFavorites;
using SuperCarga.Application.Domain.Proposals.Customers.Queries.Details;
using SuperCarga.Application.Domain.Proposals.Customers.Queries.Details.Dto;
using SuperCarga.Application.Domain.Proposals.Customers.Queries.List.All;
using SuperCarga.Application.Domain.Proposals.Customers.Queries.List.Common.Dto;
using SuperCarga.Application.Domain.Proposals.Customers.Queries.List.Favorites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Proposals.Customers.Abstraction
{
    public interface ICustomerProposalsService
    {
        Task<ListResponseDto<CustomerProposalListItemDto>> ListAllProposals(CustomerListAllProposalsQuery request);

        Task<ListResponseDto<CustomerProposalListItemDto>> ListFavoritesProposals(CustomerListFavoritesProposalsQuery request);

        Task AddProposalToFavorites(AddProposalToFavoritesCommand request);

        Task RemoveProposalFromFavorites(RemoveProposalFromFavoritesCommand request);

        Task<CustomerProposalDetailsDto> GetProposalsDetails(GetCustomerProposalDetailsQuery request);
    }
}
