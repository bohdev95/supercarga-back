using MediatR;
using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Contracts.Drivers.Abstraction;
using SuperCarga.Application.Domain.Contracts.Common.Dto;

namespace SuperCarga.Application.Domain.Contracts.Drivers.Queries.List.Finished
{
    public class DriverListFinishedContractsQueryHandler : IRequestHandler<DriverListFinishedContractsQuery, ListResponseDto<FinishedContractListITemDto>>
    {
        private readonly IDriverContractsService driverContractsService;

        public DriverListFinishedContractsQueryHandler(IDriverContractsService driverContractsService)
        {
            this.driverContractsService = driverContractsService;
        }

        public async Task<ListResponseDto<FinishedContractListITemDto>> Handle(DriverListFinishedContractsQuery request, CancellationToken cancellationToken)
        {
            var response = await driverContractsService.ListFinishedContracts(request);

            return response;
        }
    }
}
