using Microsoft.Extensions.DependencyInjection;
using SuperCarga.Application.Domain.Common.Abstraction;
using SuperCarga.Application.Domain.Contracts.Common.Abstraction;
using SuperCarga.Application.Domain.Contracts.Customers.Abstraction;
using SuperCarga.Application.Domain.Contracts.Drivers.Abstraction;
using SuperCarga.Application.Domain.Costs.Abstraction;
using SuperCarga.Application.Domain.Customers.Common.Abstraction;
using SuperCarga.Application.Domain.Customers.Customers.Abstraction;
using SuperCarga.Application.Domain.Drivers.Common.Abstraction;
using SuperCarga.Application.Domain.Drivers.Drivers.Abstraction;
using SuperCarga.Application.Domain.FreeEstimation.Abstraction;
using SuperCarga.Application.Domain.Jobs.Common.Abstraction;
using SuperCarga.Application.Domain.Jobs.Customers.Abstraction;
using SuperCarga.Application.Domain.Jobs.Drivers.Abstraction;
using SuperCarga.Application.Domain.Location.Abstraction;
using SuperCarga.Application.Domain.Proposals.Common.Abstraction;
using SuperCarga.Application.Domain.Proposals.Customers.Abstraction;
using SuperCarga.Application.Domain.Proposals.Drivers.Abstraction;
using SuperCarga.Application.Domain.Users.Abstraction;
using SuperCarga.Application.Domain.VehiculeTypes.Abstraction;
using SuperCarga.Domain.Domain.Common;
using SuperCarga.Domain.Domain.Contracts;
using SuperCarga.Domain.Domain.Costs;
using SuperCarga.Domain.Domain.Customers;
using SuperCarga.Domain.Domain.Drivers;
using SuperCarga.Domain.Domain.FreeEstimation;
using SuperCarga.Domain.Domain.Jobs;
using SuperCarga.Domain.Domain.Location;
using SuperCarga.Domain.Domain.Proposals;
using SuperCarga.Domain.Domain.Users;
using SuperCarga.Domain.Domain.VehiculeTypes;

namespace SuperCarga.Domain.Registrations
{
    public static class DomainRegistrations
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            //common
            services.AddScoped<IImagesService, ImagesService>();

            //contracts
            services.AddScoped<IContractsService, ContractsService>();
            services.AddScoped<ICustomerContractsService, CustomerContractsService>();
            services.AddScoped<IDriverContractsService, DriverContractsService>();

            //costs
            services.AddScoped<ICostsService, CostsService>();

            //customers
            services.AddScoped<ICustomersService, CustomersService>();
            services.AddScoped<ICustomersCustomersService, CustomersCustomersService>();

            //drivers
            services.AddScoped<IDriversService, DriversService>();
            services.AddScoped<IDriversDriversService, DriversDriversService>();

            //free estimation
            services.AddScoped<IFreeEstimationService, FreeEstimationService>();

            //jobs
            services.AddScoped<ICustomerJobsService, CustomerJobsService>();
            services.AddScoped<IDriverJobsService, DriverJobsService>();
            services.AddScoped<IJobsService, JobsService>();

            //location
            services.AddScoped<IDistanceService, DistanceService>();

            //proposals
            services.AddScoped<IProposalsService, ProposalsService>();
            services.AddScoped<ICustomerProposalsService, CustomerProposalsService>();
            services.AddScoped<IDriverProposalsService, DriverProposalsService>();

            //users
            services.AddAuth();
            services.AddScoped<IUsersService, UsersService>();

            //vehiculeTypes
            services.AddScoped<IVehiculeTypesService, VehiculeTypesService>();

            return services;
        }

        

    }
}
