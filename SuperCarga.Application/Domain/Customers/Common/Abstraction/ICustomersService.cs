using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Customers.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Customers.Common.Abstraction
{
    public interface ICustomersService
    {
        bool CustomerExists(Guid id);

        Task<ListResponseDto<TopCustomersDto>> GetTopCustomers(ListRequestDto request);
    }
}
