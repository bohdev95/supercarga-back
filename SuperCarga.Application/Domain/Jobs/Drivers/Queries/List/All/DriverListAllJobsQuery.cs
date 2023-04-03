using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Common.Model;
using SuperCarga.Application.Domain.Jobs.Drivers.Queries.List.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Jobs.Drivers.Queries.List.All
{
    public class DriverListAllJobsRequest : DriverListJobRequest
    {

    }

    public class DriverListAllJobsQuery : UserRequest<DriverListAllJobsRequest, ListResponseDto<DriverJobListItemDto>>
    {
    }
}
