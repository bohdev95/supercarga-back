using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperCarga.Application.Domain.FreeEstimation.Model;
using SuperCarga.Application.Domain.Jobs.Common.Models;

namespace SuperCarga.Persistence.Database.EntityConfigurations
{
    public class FreeEstimationHistoryEntityConfiguration : IEntityTypeConfiguration<FreeEstimationHistory>
    {
        public void Configure(EntityTypeBuilder<FreeEstimationHistory> builder)
        {
            builder.ToTable("free_estimation_history", SuperCargaContext.DEFAULT_SCHEMA);

            builder.HasKey(i => i.Id).HasName("PK_free_estimation_history");

            builder.Property(i => i.Id).HasColumnName("id");
            builder.Property(i => i.Created).HasColumnName("created");

            builder.Property(i => i.CargoWeight).HasColumnName("cargo_weight");
            builder.Property(i => i.CargoWidth).HasColumnName("cargo_width");
            builder.Property(i => i.CargoHeight).HasColumnName("cargo_height");
            builder.Property(i => i.CargoLenght).HasColumnName("cargo_lenght");
            builder.Property(i => i.RequireLoadingCrew).HasColumnName("require_loading_crew");
            builder.Property(i => i.RequireUnloadingCrew).HasColumnName("require_unloading_crew");
            builder.Property(i => i.Email).HasColumnName("email");
            builder.Property(i => i.CustomerName).HasColumnName("customer_name");
            builder.Property(i => i.EstimatedDistance).HasColumnName("estimated_distance");
            builder.Property(i => i.ResultVehiculeTypeId).HasColumnName("result_vehicule_type_id");
            builder.Property(i => i.ResultPricePerKm).HasColumnName("result_price_per_km");
            builder.Property(i => i.ResultEstimatedCost).HasColumnName("result_estimated_cost");
        }
    }
}
