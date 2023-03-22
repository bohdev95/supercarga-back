using MediatR;
using SuperCarga.Application.Domain.Location.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Location.Commands.CheckDistance
{
    public class CheckDistanceCommand : IRequest<CheckDistanceCommandResponse>
    {
        public AddressDto Origin { get; set; }

        public AddressDto Destination { get; set; }
    }
}
