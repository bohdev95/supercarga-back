using MediatR;
using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Common.Model;
using SuperCarga.Application.Domain.Proposals.Drivers.Queries.List.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Proposals.Drivers.Queries.List.Active
{
    public class DriverListActiveProposalsRequest : ListRequestDto
    {
    }

    public class DriverListActiveProposalsQuery : UserRequest<DriverListActiveProposalsRequest, ListResponseDto<DriverProposalListItemDto>>
    {
    }

}
