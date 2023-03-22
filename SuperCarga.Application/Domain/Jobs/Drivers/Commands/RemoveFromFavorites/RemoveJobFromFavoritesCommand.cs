using SuperCarga.Application.Domain.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Jobs.Drivers.Commands.RemoveFromFavorites
{
    public class RemoveJobFromFavoritesRequest
    {
        public Guid JobId { get; set; }
    }

    public class RemoveJobFromFavoritesCommand : UserRequest<RemoveJobFromFavoritesRequest, RemoveJobFromFavoritesCommandResponse>
    {

    }
}
