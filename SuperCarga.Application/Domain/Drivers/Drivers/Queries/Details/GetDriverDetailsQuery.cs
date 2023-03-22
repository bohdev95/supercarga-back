using MediatR;
using SuperCarga.Application.Domain.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Drivers.Drivers.Queries.Details
{
    public class GetDriverDetailsRequest
    {
    }

    public class GetDriverDetailsQuery : UserRequest<GetDriverDetailsRequest, GetDriverDetailsQueryResponse>
    {
    }
}
