using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperCarga.Application.Domain.Finances.Model;

namespace SuperCarga.Persistence.Database.EntityConfigurations
{
    public class FinanceHistoryEntityConfiguration : IEntityTypeConfiguration<FinanceHistory>
    {
        public void Configure(EntityTypeBuilder<FinanceHistory> builder)
        {
            builder.ToTable("finances_history", SuperCargaContext.DEFAULT_SCHEMA);

            builder.HasKey(i => i.Id).HasName("PK_finances_history");

            builder.Property(i => i.Id).HasColumnName("id");
            builder.Property(i => i.Created).HasColumnName("created");

            builder.Property(i => i.FinanceId).HasColumnName("finance_id");
            builder.Property(i => i.Operation).HasColumnName("operation");
            builder.Property(i => i.BalanceBefore).HasColumnName("balance_before");
            builder.Property(i => i.BalanceAfter).HasColumnName("balance_after");
            builder.Property(i => i.OperationValue).HasColumnName("operation_value");
            builder.Property(i => i.FromUserId).HasColumnName("from_user_id");
            builder.Property(i => i.ToUserId).HasColumnName("to_user_id");
            builder.Property(i => i.RelatedContractId).HasColumnName("related_contract_id");
        }
    }
}
