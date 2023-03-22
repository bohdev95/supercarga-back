using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperCarga.Application.Domain.Drivers.Common.Models;

namespace SuperCarga.Persistence.Database.EntityConfigurations
{
    public class DriverFavoriteJobEntityConfiguration : IEntityTypeConfiguration<DriverFavoriteJob>
    {
        public void Configure(EntityTypeBuilder<DriverFavoriteJob> builder)
        {
            builder.ToTable("driver_favorite_jobs", SuperCargaContext.DEFAULT_SCHEMA);

            builder.HasKey(i => new { i.DriverId, i.JobId }).HasName("PK_driver_favorite_jobs");

            builder.Property(i => i.DriverId).HasColumnName("driver_id");
            builder.Property(i => i.JobId).HasColumnName("job_id");
        }
    }
}
