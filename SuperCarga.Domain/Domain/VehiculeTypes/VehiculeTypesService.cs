using Microsoft.EntityFrameworkCore;
using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.VehiculeTypes.Abstraction;
using SuperCarga.Application.Domain.VehiculeTypes.Dto;
using SuperCarga.Application.Domain.VehiculeTypes.Queries.List.Dto;
using SuperCarga.Persistence.Database;

namespace SuperCarga.Domain.Domain.VehiculeTypes
{
    public class VehiculeTypesService : IVehiculeTypesService
    {
        private readonly SuperCargaContext ctx;

        public VehiculeTypesService(SuperCargaContext ctx)
        {
            this.ctx = ctx;
        }

        public bool VehiculeTypeExists(Guid id) => ctx.VehiculeTypes.Where(x => x.Id == id).Any();

        public async Task<List<VehiculeTypeListDto>> ListVehiculeTypes()
        {
            var dtos = await ctx.VehiculeTypes
                .Select(x => new VehiculeTypeListDto
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToListAsync();

            return dtos;
        }

        public async Task<VehiculeTypeDto> CheckVehiculeType(CargoDimensionsDto cargo)
        {
            var vehicule = await ctx.VehiculeTypes
                .Where(x => x.MaxCargoWeight > cargo.Weight)
                .Where(x => x.MaxCargoLenght > cargo.Lenght)
                .Where(x => x.MaxCargoWidth > cargo.Width)
                .Where(x => x.MaxCargoHeight > cargo.Height)
                .OrderBy(x => x.MaxCargoWeight)
                .Select(x => new VehiculeTypeDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    PricePerKm = x.PricePerKm
                })
                .FirstOrDefaultAsync();

            return vehicule;
        }
    }
}
