using SuperCarga.Application.Domain.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Proposals.Customers.Commands.AddToFavorites
{
    public class AddProposalToFavoritesRequest
    {
        public Guid ProposalId { get; set; }
    }

    public class AddProposalToFavoritesCommand : UserRequest<AddProposalToFavoritesRequest, AddProposalToFavoritesCommandResponse>
    {

    }
}
