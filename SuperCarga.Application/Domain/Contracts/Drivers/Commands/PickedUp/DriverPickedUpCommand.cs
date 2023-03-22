using MediatR;
using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Contracts.Drivers.Commands.PickedUp
{
    public class DriverPickedUpRequest
    {
        public Guid ContractId { get; set; }

        public ImageDto CargoImage { get; set; }

        public ImageDto ProofImage { get; set; }
    }

    public class DriverPickedUpCommand : UserRequest<DriverPickedUpRequest, DriverPickedUpCommandResponse>
    {
    }
}
