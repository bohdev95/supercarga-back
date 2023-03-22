using MediatR;
using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Contracts.Drivers.Abstraction;
using SuperCarga.Application.Domain.Contracts.Common.Dto;

namespace SuperCarga.Application.Domain.Contracts.Drivers.Queries.List.Active
{
    public class DriverListActiveContractsQueryHandler : IRequestHandler<DriverListActiveContractsQuery, ListResponseDto<ActiveContractListITemDto>>
    {
        private readonly IDriverContractsService driverContractsService;

        public DriverListActiveContractsQueryHandler(IDriverContractsService driverContractsService)
        {
            this.driverContractsService = driverContractsService;
        }

        public async Task<ListResponseDto<ActiveContractListITemDto>> Handle(DriverListActiveContractsQuery request, CancellationToken cancellationToken)
        {
            var response = await driverContractsService.ListActiveContracts(request);

            return response;
        }
    }
}
