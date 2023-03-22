using MediatR;
using SuperCarga.Application.Domain.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Contracts.Drivers.Queries.Details
{
    public class GetDriverContractDetailsRequest
    {
        public Guid ContractId { get; set; }
    }


    public class GetDriverContractDetailsQuery : UserRequest<GetDriverContractDetailsRequest, GetDriverContractDetailsQueryResponse>
    {
    }
}
