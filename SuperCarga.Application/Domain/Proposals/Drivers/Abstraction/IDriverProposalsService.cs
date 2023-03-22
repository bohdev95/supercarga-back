using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Proposals.Drivers.Commands.Add;
using SuperCarga.Application.Domain.Proposals.Drivers.Queries.Details;
using SuperCarga.Application.Domain.Proposals.Drivers.Queries.Details.Dto;
using SuperCarga.Application.Domain.Proposals.Drivers.Queries.List.Active;
using SuperCarga.Application.Domain.Proposals.Drivers.Queries.List.Archived;
using SuperCarga.Application.Domain.Proposals.Drivers.Queries.List.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Proposals.Drivers.Abstraction
{
    public interface IDriverProposalsService
    {
        Task<ListResponseDto<DriverProposalListItemDto>> ListActiveProposals(DriverListActiveProposalsQuery request);

        Task<ListResponseDto<DriverProposalListItemDto>> ListArchivedProposals(DriverListArchivedProposalsQuery request);

        Task<Guid> AddProposal(AddProposalCommand request);

        Task<DriverProposalDetailsDto> GetProposalsDetails(GetDriverProposalDetailsQuery request);
    }
}
