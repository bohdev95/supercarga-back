using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Drivers.Common.Dto;

namespace SuperCarga.Application.Domain.Drivers.Common.Abstraction
{
    public interface IDriversService
    {
        bool DriverExists(Guid id);

        Task UpdateDriverRates(Guid id, bool save);

        Task<ListResponseDto<TopRatedDriverDto>> GetTopRatedDrivers(ListRequestDto request);
    }
}
