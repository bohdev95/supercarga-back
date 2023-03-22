using Microsoft.EntityFrameworkCore;
using SuperCarga.Application.Domain.Contracts.Common.Models;
using SuperCarga.Application.Domain.Costs.Model;
using SuperCarga.Application.Domain.Customers.Common.Models;
using SuperCarga.Application.Domain.Drivers.Common.Models;
using SuperCarga.Application.Domain.Finances.Model;
using SuperCarga.Application.Domain.FreeEstimation.Model;
using SuperCarga.Application.Domain.Jobs.Common.Models;
using SuperCarga.Application.Domain.Proposals.Common.Models;
using SuperCarga.Application.Domain.Users.Models;
using SuperCarga.Application.Domain.VehiculeTypes.Models;
using SuperCarga.Persistence.Database.EntityConfigurations;

namespace SuperCarga.Persistence.Database
{
    public class SuperCargaContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "sc";
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobAdditionalCost> JobAdditionalCosts { get; set; }
        public DbSet<FreeEstimationHistory> FreeEstimationHistory { get; set; }
        public DbSet<VehiculeType> VehiculeTypes { get; set; }
        public DbSet<Proposal> Proposals { get; set; }
        public DbSet<ContractHistory> ContractHistories { get; set; }
        public DbSet<ContractAdditionalCost> ContractAdditionalCosts { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Finance> Finances { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Cost> Costs { get; set; }


        public SuperCargaContext(DbContextOptions<SuperCargaContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new JobEntityConfiguration());
            modelBuilder.ApplyConfiguration(new JobAdditionalCostEntityConfiguration());
            modelBuilder.ApplyConfiguration(new FreeEstimationHistoryEntityConfiguration());
            modelBuilder.ApplyConfiguration(new VehiculeTypeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProposalEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ContractHistoryEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ContractEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RoleEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleEntityConfiguration());
            modelBuilder.ApplyConfiguration(new FinanceEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerFavoriteProposalEntityConfiguration());
            modelBuilder.ApplyConfiguration(new DriverEntityConfiguration());
            modelBuilder.ApplyConfiguration(new DriverFavoriteJobEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CostEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ContractAdditionalCostEntityConfiguration());
        }
    }
}
