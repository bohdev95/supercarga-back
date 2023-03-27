using SuperCarga.Application.Domain.Location.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Location.Abstraction
{
    public interface IDistanceService
    {
        Task<int?> CheckDistance(AddressDto origin, AddressDto destination);
    }
}
