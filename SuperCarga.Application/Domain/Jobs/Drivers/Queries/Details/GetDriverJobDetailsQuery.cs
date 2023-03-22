using MediatR;
using SuperCarga.Application.Domain.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Jobs.Drivers.Queries.Details
{
    public class GetDriverJobsDetailsRequest
    {
        public Guid JobId { get; set; }
    }

    public class GetDriverJobsDetailsQuery : UserRequest<GetDriverJobsDetailsRequest, GetDriverJobsDetailsQueryResponse>
    {
    }
}
