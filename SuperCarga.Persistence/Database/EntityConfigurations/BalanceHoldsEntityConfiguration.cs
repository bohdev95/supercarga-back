using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperCarga.Application.Domain.Finances.Model;

namespace SuperCarga.Persistence.Database.EntityConfigurations
{
    public class BalanceHoldsEntityConfiguration : IEntityTypeConfiguration<BalanceHold>
    {
        public void Configure(EntityTypeBuilder<BalanceHold> builder)
        {
            builder.ToTable("balance_holds", SuperCargaContext.DEFAULT_SCHEMA);

            builder.HasKey(i => i.Id).HasName("PK_balance_holds");

            builder.Property(i => i.Id).HasColumnName("id");
            builder.Property(i => i.Created).HasColumnName("created");

            builder.Property(i => i.FinanceId).HasColumnName("finance_id");
            builder.Property(i => i.Value).HasColumnName("value");
            builder.Property(i => i.RelatedContractId).HasColumnName("related_contract_id");

            builder.HasOne(i => i.RelatedContract).WithOne().HasForeignKey<BalanceHold>(i => i.RelatedContractId);
        }
    }
}
