using SuperCarga.Application.Domain.Jobs.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Common.Dto
{
    public static class CargoDimensionsDtoExtensions
    {
        public static CargoDimensionsDto GetDimensionsCargo(this Job post) => new CargoDimensionsDto
        {
            Width = post.CargoWidth,
            Height = post.CargoHeight,
            Lenght = post.CargoLenght,
            Weight = post.CargoWeight
        };
    }
}
