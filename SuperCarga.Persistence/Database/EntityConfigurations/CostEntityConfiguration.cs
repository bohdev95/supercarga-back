using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperCarga.Application.Domain.Costs.Model;

namespace SuperCarga.Persistence.Database.EntityConfigurations
{
    public class CostEntityConfiguration : IEntityTypeConfiguration<Cost>
    {
        public void Configure(EntityTypeBuilder<Cost> builder)
        {
            builder.ToTable("costs", SuperCargaContext.DEFAULT_SCHEMA);

            builder.HasKey(i => i.Id).HasName("PK_costs");

            builder.Property(i => i.Id).HasColumnName("id");
            builder.Property(i => i.Created).HasColumnName("created");

            builder.Property(i => i.Type).HasColumnName("type");
            builder.Property(i => i.Value).HasColumnName("value");
            builder.Property(i => i.FromDate).HasColumnName("from_date");
            builder.Property(i => i.ToDate).HasColumnName("to_date");
        }
    }
}
