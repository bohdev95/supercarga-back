using MediatR;
using SuperCarga.Application.Domain.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Customers.Customers.Queries.Details
{
    public class GetCustomersDetailsRequest
    {
    }

    public class GetCustomersDetailsQuery : UserRequest<GetCustomersDetailsRequest, GetCustomersDetailsQueryResponse>
    {
    }
}
