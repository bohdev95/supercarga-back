using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.FreeEstimation.Commands.Calculate.Dto
{
    public class FreeEstimationCalculateDto
    {
        public string VehiculeTypeName { get; set; }

        public decimal Cost { get; set; }
    }
}
