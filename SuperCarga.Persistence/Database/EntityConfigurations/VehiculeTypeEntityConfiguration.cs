using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperCarga.Application.Domain.VehiculeTypes.Models;

namespace SuperCarga.Persistence.Database.EntityConfigurations
{
    public class VehiculeTypeEntityConfiguration : IEntityTypeConfiguration<VehiculeType>
    {
        public void Configure(EntityTypeBuilder<VehiculeType> builder)
        {
            builder.ToTable("vehicule_types", SuperCargaContext.DEFAULT_SCHEMA);

            builder.HasKey(i => i.Id).HasName("PK_vehicule_types");

            builder.Property(i => i.Id).HasColumnName("id");
            builder.Property(i => i.Created).HasColumnName("created");

            builder.Property(i => i.Name).HasColumnName("name");
            builder.Property(i => i.PricePerKm).HasColumnName("price_per_km");
            builder.Property(i => i.MaxCargoWeight).HasColumnName("max_cargo_weight");
            builder.Property(i => i.MaxCargoLenght).HasColumnName("max_cargo_lenght");
            builder.Property(i => i.MaxCargoWidth).HasColumnName("max_cargo_width");
            builder.Property(i => i.MaxCargoHeight).HasColumnName("max_cargo_height");
            builder.Property(i => i.RequireDocuments).HasColumnName("require_documents");
        }
    }
}
