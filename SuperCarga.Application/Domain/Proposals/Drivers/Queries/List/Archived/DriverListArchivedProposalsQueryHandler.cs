using MediatR;
using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Proposals.Drivers.Abstraction;
using SuperCarga.Application.Domain.Proposals.Drivers.Queries.List.Common.Dto;

namespace SuperCarga.Application.Domain.Proposals.Drivers.Queries.List.Archived
{
    public class DriverListArchivedProposalsQueryHandler : IRequestHandler<DriverListArchivedProposalsQuery, ListResponseDto<DriverProposalListItemDto>>
    {
        private readonly IDriverProposalsService driverProposalsService;

        public DriverListArchivedProposalsQueryHandler(IDriverProposalsService driverProposalsService)
        {
            this.driverProposalsService = driverProposalsService;
        }

        public async Task<ListResponseDto<DriverProposalListItemDto>> Handle(DriverListArchivedProposalsQuery request, CancellationToken cancellationToken)
        {
            var response = await driverProposalsService.ListArchivedProposals(request);

            return response;
        }
    }
}
