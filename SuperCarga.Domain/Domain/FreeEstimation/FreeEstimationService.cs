using SuperCarga.Application.Domain.FreeEstimation.Abstraction;
using SuperCarga.Application.Domain.FreeEstimation.Commands.Calculate;
using SuperCarga.Application.Domain.FreeEstimation.Commands.Calculate.Dto;
using SuperCarga.Application.Domain.FreeEstimation.Model;
using SuperCarga.Application.Domain.VehiculeTypes.Abstraction;
using SuperCarga.Application.Domain.VehiculeTypes.Dto;
using SuperCarga.Persistence.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Domain.Domain.FreeEstimation
{
    public class FreeEstimationService : IFreeEstimationService
    {
        private readonly SuperCargaContext ctx;
        private readonly IVehiculeTypesService vehiculeTypesService;

        public FreeEstimationService(SuperCargaContext ctx, IVehiculeTypesService vehiculeTypesService)
        {
            this.ctx = ctx;
            this.vehiculeTypesService = vehiculeTypesService;
        }

        public async Task<FreeEstimationCalculateDto> Calculate(FreeEstimationCalculateCommand request)
        {
            var result = new FreeEstimationCalculateDto();

            var vehicule = await vehiculeTypesService.CheckVehiculeType(request.Cargo);
            result.VehiculeTypeName = vehicule.Name;

            if(vehicule.PricePerKm > 0)
            {
                result.Cost = vehicule.PricePerKm * request.EstimatedDistance; //TODO calculation cost service
            }

            await AddCalculationToHistory(request, result, vehicule);

            return result;
        }

        private async Task AddCalculationToHistory(FreeEstimationCalculateCommand request, FreeEstimationCalculateDto result, VehiculeTypeDto vehicule)
        {
            var history = new FreeEstimationHistory
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                CargoHeight = request.Cargo.Height,
                CargoLenght = request.Cargo.Lenght,
                CargoWeight = request.Cargo.Weight,
                CargoWidth = request.Cargo.Width,
                EstimatedDistance = request.EstimatedDistance,
                RequireLoadingCrew = request.RequireLoadingCrew,
                RequireUnloadingCrew = request.RequireUnloadingCrew,
                Email = request.Email,
                CustomerName = request.CustomerName,
                ResultVehiculeTypeId = vehicule.Id,
                ResultPricePerKm = vehicule.PricePerKm,
                ResultEstimatedCost = result.Cost
            };

            await ctx.AddAsync(history);
            await ctx.SaveChangesAsync();
        }
    }
}
