using MediatR;
using SuperCarga.Application.Domain.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Proposals.Customers.Queries.Details
{
    public class GetCustomerProposalDetailsRequest
    {
        public Guid ProposalId { get; set; }
    }

    public class GetCustomerProposalDetailsQuery : UserRequest<GetCustomerProposalDetailsRequest, GetCustomerProposalDetailsQueryResponse>
    {
    }
}
