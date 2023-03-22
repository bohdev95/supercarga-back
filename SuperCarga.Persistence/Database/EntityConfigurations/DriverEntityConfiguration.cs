using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperCarga.Application.Domain.Drivers.Common.Models;
using SuperCarga.Application.Domain.Users.Models;

namespace SuperCarga.Persistence.Database.EntityConfigurations
{
    public class DriverEntityConfiguration : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {
            builder.ToTable("drivers", SuperCargaContext.DEFAULT_SCHEMA);

            builder.HasKey(i => i.Id).HasName("PK_drivers");

            builder.Property(i => i.Id).HasColumnName("id");
            builder.Property(i => i.Created).HasColumnName("created");
            
            builder.Property(i => i.VehiculeTypeId).HasColumnName("vehicule_type_id");
            builder.Property(i => i.DrivingLicensePath).HasColumnName("driving_license_path");
            builder.Property(i => i.Contracts).HasColumnName("contracts");
            builder.Property(i => i.RatedContracts).HasColumnName("rated_contracts");
            builder.Property(i => i.Rating).HasColumnName("rating");

            builder.HasOne(i => i.User).WithOne().HasForeignKey<User>(c => c.DriverId);
            builder.HasOne(i => i.VehiculeType).WithMany().HasForeignKey(i => i.VehiculeTypeId);

            builder
                .HasMany(x => x.FavoriteJobs)
                .WithMany(x => x.AddedToFavoriteBy)
                .UsingEntity<DriverFavoriteJob>(
                    x =>
                        x.HasOne(x => x.Job)
                        .WithMany()
                        .HasForeignKey(x => x.JobId),
                    x =>
                        x.HasOne(x => x.Driver)
                        .WithMany()
                        .HasForeignKey(x => x.DriverId));
        }
    }
}
