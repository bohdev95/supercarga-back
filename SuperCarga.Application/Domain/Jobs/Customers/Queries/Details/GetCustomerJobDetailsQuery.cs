using MediatR;
using SuperCarga.Application.Domain.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Jobs.Customers.Queries.Details
{
    public class GetCustomerJobsDetailsRequest
    {
        public Guid JobId { get; set; }
    }

    public class GetCustomerJobsDetailsQuery : UserRequest<GetCustomerJobsDetailsRequest, GetCustomerJobsDetailsQueryResponse>
    {
    }
}
