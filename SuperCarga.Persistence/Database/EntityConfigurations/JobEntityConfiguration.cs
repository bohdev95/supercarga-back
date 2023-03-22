using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperCarga.Application.Domain.Contracts.Common.Models;
using SuperCarga.Application.Domain.Drivers.Common.Models;
using SuperCarga.Application.Domain.Jobs.Common.Models;

namespace SuperCarga.Persistence.Database.EntityConfigurations
{

    public class JobEntityConfiguration : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.ToTable("jobs", SuperCargaContext.DEFAULT_SCHEMA);

            builder.HasKey(i => i.Id).HasName("PK_jobs");

            builder.Property(i => i.Id).HasColumnName("id");
            builder.Property(i => i.Created).HasColumnName("created");

            builder.Property(i => i.CustomerId).HasColumnName("customer_id");
            builder.Property(i => i.CargoWeight).HasColumnName("cargo_weight");
            builder.Property(i => i.CargoWidth).HasColumnName("cargo_width");
            builder.Property(i => i.CargoHeight).HasColumnName("cargo_height");
            builder.Property(i => i.CargoLenght).HasColumnName("cargo_lenght");
            builder.Property(i => i.OriginCity).HasColumnName("origin_city");
            builder.Property(i => i.OriginStreet).HasColumnName("origin_street");
            builder.Property(i => i.OriginPostCode).HasColumnName("origin_post_code");
            builder.Property(i => i.DestinationCity).HasColumnName("destination_city");
            builder.Property(i => i.DestinationStreet).HasColumnName("destination_street");
            builder.Property(i => i.DestinationPostCode).HasColumnName("destination_post_code");
            builder.Property(i => i.RequireLoadingCrew).HasColumnName("require_loading_crew");
            builder.Property(i => i.RequireUnloadingCrew).HasColumnName("require_unloading_crew");
            builder.Property(i => i.Description).HasColumnName("description");
            builder.Property(i => i.Tittle).HasColumnName("tittle");
            builder.Property(i => i.Distance).HasColumnName("distance");
            builder.Property(i => i.State).HasColumnName("state");
            builder.Property(i => i.VehiculeTypeId).HasColumnName("vehicule_type_id");
            builder.Property(i => i.PricePerKm).HasColumnName("price_per_km");
            builder.Property(i => i.PricePerDistance).HasColumnName("price_per_distance");
            builder.Property(i => i.TotalPrice).HasColumnName("total_price");
            builder.Property(i => i.ServiceFee).HasColumnName("service_fee");
            builder.Property(i => i.Price).HasColumnName("price");
            builder.Property(i => i.PickupDate).HasColumnName("pickup_date");
            builder.Property(i => i.DeliveryDate).HasColumnName("delivery_date");
            builder.Property(i => i.CargoImagePath).HasColumnName("cargo_image_path");

            builder.HasOne(i => i.Customer).WithMany().HasForeignKey(i => i.CustomerId);
            builder.HasMany(i => i.Contracts).WithOne().HasForeignKey(c => c.JobId);
            builder.HasMany(i => i.Proposals).WithOne().HasForeignKey(i => i.JobId);
            builder.HasOne(i => i.VehiculeType).WithMany().HasForeignKey(i => i.VehiculeTypeId);
            builder.HasMany(i => i.Additions).WithOne().HasForeignKey(c => c.JobId);

            builder
                .HasMany(x => x.AddedToFavoriteBy)
                .WithMany(x => x.FavoriteJobs)
                .UsingEntity<DriverFavoriteJob>(
                    x =>
                        x.HasOne(x => x.Driver)
                        .WithMany()
                        .HasForeignKey(x => x.DriverId),
                    x =>
                        x.HasOne(x => x.Job)
                        .WithMany()
                        .HasForeignKey(x => x.JobId));
        }
    }
}
