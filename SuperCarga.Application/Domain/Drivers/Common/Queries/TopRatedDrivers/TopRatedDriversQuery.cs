using MediatR;
using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Drivers.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Drivers.Common.Queries.TopRatedDrivers
{

    public class TopRatedDriversQuery : ListRequestDto, IRequest<ListResponseDto<TopRatedDriverDto>>
    {
        
    }
}
