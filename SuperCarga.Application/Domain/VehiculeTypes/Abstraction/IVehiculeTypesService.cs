using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.VehiculeTypes.Dto;
using SuperCarga.Application.Domain.VehiculeTypes.Queries.List.Dto;

namespace SuperCarga.Application.Domain.VehiculeTypes.Abstraction
{
    public interface IVehiculeTypesService
    {
        bool VehiculeTypeExists(Guid id);

        Task<List<VehiculeTypeListDto>> ListVehiculeTypes();

        Task<VehiculeTypeDto> CheckVehiculeType(CargoDimensionsDto cargo);
    }
}
