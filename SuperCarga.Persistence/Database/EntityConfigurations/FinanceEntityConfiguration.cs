using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperCarga.Application.Domain.Finances.Model;

namespace SuperCarga.Persistence.Database.EntityConfigurations
{
    public class FinanceEntityConfiguration : IEntityTypeConfiguration<Finance>
    {
        public void Configure(EntityTypeBuilder<Finance> builder)
        {
            builder.ToTable("finances", SuperCargaContext.DEFAULT_SCHEMA);

            builder.HasKey(i => i.Id).HasName("PK_finances");

            builder.Property(i => i.Id).HasColumnName("id");
            builder.Property(i => i.Created).HasColumnName("created");

            builder.Property(i => i.UserId).HasColumnName("user_id");
            builder.Property(i => i.Balance).HasColumnName("balance");
            builder.Property(i => i.AvailableBalance).HasColumnName("available_balance");

            builder.HasMany(i => i.Holds).WithOne().HasForeignKey(c => c.FinanceId);
        }
    }
}
