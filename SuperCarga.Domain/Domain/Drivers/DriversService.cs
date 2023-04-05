using Microsoft.EntityFrameworkCore;
using SuperCarga.Application.Domain.Common.Abstraction;
using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Drivers.Common.Abstraction;
using SuperCarga.Application.Domain.Drivers.Common.Dto;
using SuperCarga.Domain.Domain.Common;
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

        public async Task<ListResponseDto<TopRatedDriverDto>> GetTopRatedDrivers(ListRequestDto request)
        {
            var drivers = await ctx.Drivers
                .Include(x => x.User)
                .Where(x => x.Rating.HasValue)
                .Select(x => new TopRatedDriverDto
                {
                    Id = x.Id,
                    Name = x.User.FirstName,
                    LastName = x.User.LastName,
                    ImagePath = x.User.ImagePath,
                    Earnings = x.Earnings,
                    Rating = x.Rating.Value
                })
                .OrderByDescending(x => x.Rating)
                .Paginate(request);

            return drivers;
        }

        public async Task UpdateDriverRates(Guid id, bool save)
        {
            var driver = await ctx.Drivers
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            var driverContracts = await ctx.Contracts
                .Where(x => x.DriverId == id)
                .ToListAsync();

            var driversRatesdContracts = driverContracts
                .Where(x => x.Rating.HasValue)
                .ToList();

            var driversRates = driversRatesdContracts
                .Select(x => x.Rating)
                .ToList();

            var earnings = driversRatesdContracts.Sum(x => x.Price);
            var driverContractsCount = driverContracts.Count();
            var driverRatesCount = driversRates.Count();
            decimal? rating = driverRatesCount > 0
                ? Math.Round(driversRates.Sum(c => c.Value) / driverRatesCount, 2)
                : null;


            driver.Contracts = driverContractsCount;
            driver.RatedContracts = driverRatesCount;
            driver.Rating = rating;
            driver.Earnings = earnings;

            if(save)
            {
                await ctx.SaveChangesAsync();
            }
        }
    }
}
