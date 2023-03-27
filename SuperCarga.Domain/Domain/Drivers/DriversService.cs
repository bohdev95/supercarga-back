using Microsoft.EntityFrameworkCore;
using SuperCarga.Application.Domain.Common.Abstraction;
using SuperCarga.Application.Domain.Drivers.Common.Abstraction;
using SuperCarga.Domain.Domain.Users;
using SuperCarga.Persistence.Database;

namespace SuperCarga.Domain.Domain.Drivers
{
    public class DriversService : UsersService, IDriversService
    {
        public DriversService(SuperCargaContext ctx, IImagesService imagesService) : base(ctx, imagesService)
        {
        }

        public bool DriverExists(Guid id) => ctx.Drivers.Where(x => x.Id == id).Any();

        public async Task UpdateDriverRates(Guid id, bool save)
        {
            var driver = await ctx.Drivers
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            var driverContracts = await ctx.Contracts
                .Where(x => x.DriverId == id)
                .ToListAsync();

            var driversRates = driverContracts
                .Where(x => x.Rating.HasValue)
                .Select(x => x.Rating)
                .ToList();

            var driverContractsCount = driverContracts.Count();
            var driverRatesCount = driversRates.Count();
            decimal? rating = driverRatesCount > 0
                ? Math.Round(driversRates.Sum(c => c.Value) / driverRatesCount, 2)
                : null;

            driver.Contracts = driverContractsCount;
            driver.RatedContracts = driverRatesCount;
            driver.Rating = rating;

            if(save)
            {
                await ctx.SaveChangesAsync();
            }
        }
    }
}
