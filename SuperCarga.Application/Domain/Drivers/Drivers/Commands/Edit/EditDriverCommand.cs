using MediatR;
using SuperCarga.Application.Domain.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Drivers.Drivers.Commands.Edit
{
    public class EditDriverRequest
    {
        public Guid VehiculeTypeId { get; set; }
    }

    public class EditDriverCommand : UserRequest<EditDriverRequest, EditDriverCommandResponse>
    {
    }
}
