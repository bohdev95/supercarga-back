using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperCarga.Application.Domain.Jobs.Common.Models;

namespace SuperCarga.Persistence.Database.EntityConfigurations
{
    public class JobAdditionalCostEntityConfiguration : IEntityTypeConfiguration<JobAdditionalCost>
    {
        public void Configure(EntityTypeBuilder<JobAdditionalCost> builder)
        {
            builder.ToTable("job_additional_costs", SuperCargaContext.DEFAULT_SCHEMA);

            builder.HasKey(i => i.Id).HasName("PK_job_additional_costs");

            builder.Property(i => i.Id).HasColumnName("id");
            builder.Property(i => i.Created).HasColumnName("created");
            
            builder.Property(i => i.JobId).HasColumnName("job_id");
            builder.Property(i => i.Name).HasColumnName("name");
            builder.Property(i => i.Price).HasColumnName("price");
        }
    }
}
