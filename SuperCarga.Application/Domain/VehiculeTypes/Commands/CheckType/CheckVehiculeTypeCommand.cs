using MediatR;
using SuperCarga.Application.Domain.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.VehiculeTypes.Commands.CheckType
{
    public class CheckVehiculeTypeCommand : CargoDimensionsDto, IRequest<CheckVehiculeTypeCommandResponse>
    {
    }
}
