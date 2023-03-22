using SuperCarga.Application.Domain.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Jobs.Customers.Commands.Add.Dto
{
    public class AddJobCargoDto : CargoDimensionsDto
    {
        public ImageDto? Image { get; set; }
    }
}
