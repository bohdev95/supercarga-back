using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SuperCarga.Application.Domain.Costs.Abstraction;
using SuperCarga.Application.Domain.Drivers.Common.Abstraction;
using SuperCarga.Application.Settings;
using SuperCarga.Persistence.Database;
using SuperCarga.Persistence.Database.Init;

namespace SuperCarga.Persistence.Registrations
{
    public static class PersistanceRegistrations
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services, string cs)
        {
            services.AddDbContext<SuperCargaContext>(options => options.UseNpgsql(cs));

            services.SeedDb();

            return services;
        }

        //TODO if development
        private static void SeedDb(this IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            var ctx = sp.GetService<SuperCargaContext>();
            var imageStoreConfig = sp.GetService<ImageStoresConfig>();
            var costsService = sp.GetService<ICostsService>();
            var driversService = sp.GetService<IDriversService>();

            ctx.Database.EnsureCreated();

            ctx.AddCosts();
            ctx.AddVechiculeTypes();
            ctx.AddRoles();
            ctx.AddUsers(imageStoreConfig.Users);
            ctx.AddJobs(costsService, driversService, 2500); 
        }
    }
}
