using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SuperCarga.Application.Domain.Common.Abstraction;
using SuperCarga.Application.Domain.Drivers.Common.Models;
using SuperCarga.Application.Domain.Users.Commands.Register;
using SuperCarga.Application.Domain.Users.Models;
using SuperCarga.Persistence.Database;
using SuperCarga.Application.Domain.Drivers.Drivers.Commands.Edit;
using SuperCarga.Application.Domain.Drivers.Drivers.Abstraction;
using SuperCarga.Application.Domain.Drivers.Drivers.Queries.Details.Dro;
using SuperCarga.Application.Domain.Drivers.Drivers.Queries.Details;
using SuperCarga.Application.Domain.Drivers.Drivers.Commands.UploadDrivingLicense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SuperCarga.Domain.Domain.Drivers
{
    public class DriversDriversService : DriversService, IDriversDriversService
    {
        public DriversDriversService(SuperCargaContext ctx, IImagesService imagesService) : base(ctx, imagesService)
        {
        }

        public async Task EditDriver(EditDriverCommand request)
        {
            var driver = await ctx.Drivers
                .Where(x => x.Id == request.User.DriverId)
                .FirstOrDefaultAsync();

            driver.VehiculeTypeId = request.Data.VehiculeTypeId;

            await ctx.SaveChangesAsync();
        }

        public async Task<DriverDetailsDto> GetDriverDetails(GetDriverDetailsQuery request)
        {
            var driver = await ctx.Drivers
                .Include(x => x.VehiculeType)
                .Include(x => x.User)
                .ThenInclude(x => x.Finance)
                .Where(x => x.Id == request.User.DriverId)
                .Select(x => new DriverDetailsDto
                {
                    Id = x.Id,
                    Created = x.Created,
                    RatedContracts = x.RatedContracts,
                    Rating = x.Rating,
                    Name = x.User.DisplayName(),
                    Email = x.User.Email,
                    ImagePath = x.User.ImagePath,
                    DriverLicensePath = x.DrivingLicensePath,
                    VehiculeTypeName = x.VehiculeType.Name,
                    VerifivationState = x.User.VerificationState,
                    Balance = x.User.Finance.Balance
                })
                .FirstOrDefaultAsync();

            return driver;
        }

        public async Task<Guid> CreateDriver(RegisterUserCommand request, Guid vehivuleId)
        {
            var driver = new Driver
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                VehiculeTypeId = vehivuleId
            };

            await ctx.Drivers.AddAsync(driver);

            var driverRole = await ctx.Roles
                .Where(x => x.Name == Roles.Driver)
                .SingleAsync();

            await CreateUser(request, user =>
            {
                user.DriverId = driver.Id;
                user.Roles = new List<Role> { driverRole };
            });

            return driver.Id;
        }

        public async Task<string> UploadDrivingLicense(UploadDrivingLicenseCommand request)
        {
            var driver = await ctx.Drivers
                .Where(x => x.Id == request.User.DriverId.Value)
                .FirstOrDefaultAsync();

            var driverLicensePath = await imagesService
                .SaveDrivingLicenseImage(request.Data.DrivingLicense, request.User.Id);

            driver.DrivingLicensePath = driverLicensePath;

            await ctx.SaveChangesAsync();

            return driverLicensePath;
        }
    }
}
