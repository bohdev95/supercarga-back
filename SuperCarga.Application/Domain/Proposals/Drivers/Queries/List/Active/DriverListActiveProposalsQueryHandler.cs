using MediatR;
using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Proposals.Drivers.Abstraction;
using SuperCarga.Application.Domain.Proposals.Drivers.Queries.List.Common.Dto;

namespace SuperCarga.Application.Domain.Proposals.Drivers.Queries.List.Active
{
    public class DriverListActiveProposalsQueryHandler : IRequestHandler<DriverListActiveProposalsQuery, ListResponseDto<DriverProposalListItemDto>>
    {
        private readonly IDriverProposalsService driverProposalsService;

        public DriverListActiveProposalsQueryHandler(IDriverProposalsService driverProposalsService)
        {
            this.driverProposalsService = driverProposalsService;
        }

        public async Task<ListResponseDto<DriverProposalListItemDto>> Handle(DriverListActiveProposalsQuery request, CancellationToken cancellationToken)
        {
            var response =  await driverProposalsService.ListActiveProposals(request);

            return response;
        }
    }
}
