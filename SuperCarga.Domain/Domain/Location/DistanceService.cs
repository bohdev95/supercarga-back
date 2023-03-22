using SuperCarga.Application.Domain.Location.Abstraction;
using SuperCarga.Application.Domain.Location.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Domain.Domain.Location
{
    public class DistanceService : IDistanceService
    {
        public async Task<int> CheckDistance(AddressDto origin, AddressDto destination)
        {
            //TODO 

            var r = new Random();

            return r.Next(100, 10000);
        }
    }
}
