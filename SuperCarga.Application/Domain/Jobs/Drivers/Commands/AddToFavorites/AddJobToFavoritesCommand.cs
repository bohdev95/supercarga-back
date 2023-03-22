using SuperCarga.Application.Domain.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Jobs.Drivers.Commands.AddToFavorites
{
    public class AddJobToFavoritesRequest
    {
        public Guid JobId { get; set; }
    }

    public class AddJobToFavoritesCommand : UserRequest<AddJobToFavoritesRequest, AddJobToFavoritesCommandResponse>
    {

    }
}
