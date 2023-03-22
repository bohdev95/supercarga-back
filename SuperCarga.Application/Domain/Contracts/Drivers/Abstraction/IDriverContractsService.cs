using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Contracts.Drivers.Commands.Delivered;
using SuperCarga.Application.Domain.Contracts.Drivers.Commands.PickedUp;
using SuperCarga.Application.Domain.Contracts.Drivers.Commands.Start;
using SuperCarga.Application.Domain.Contracts.Drivers.Queries.Details;
using SuperCarga.Application.Domain.Contracts.Drivers.Queries.Details.Dto;
using SuperCarga.Application.Domain.Contracts.Drivers.Queries.List.Active;
using SuperCarga.Application.Domain.Contracts.Common.Dto;
using SuperCarga.Application.Domain.Contracts.Drivers.Queries.List.Finished;

namespace SuperCarga.Application.Domain.Contracts.Drivers.Abstraction
{
    public interface IDriverContractsService
    {
        Task<ListResponseDto<ActiveContractListITemDto>> ListActiveContracts(DriverListActiveContractsQuery request);

        Task<ListResponseDto<FinishedContractListITemDto>> ListFinishedContracts(DriverListFinishedContractsQuery request);

        Task<DriverContractDetailsDto> GetContractDetails(GetDriverContractDetailsQuery request);

        Task Delivered(DriverDeliveredCommand request);

        Task PickedUp(DriverPickedUpCommand request);

        Task StartContract(StartContractCommand request);
    }
}
