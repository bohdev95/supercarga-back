using SuperCarga.Application.Domain.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Proposals.Customers.Commands.RemoveFromFavorites
{
    public class RemoveProposalFromFavoritesRequest
    {
        public Guid ProposalId { get; set; }
    }

    public class RemoveProposalFromFavoritesCommand : UserRequest<RemoveProposalFromFavoritesRequest, RemoveProposalFromFavoritesCommandResponse>
    {

    }
}
