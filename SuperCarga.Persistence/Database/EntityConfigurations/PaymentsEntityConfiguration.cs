using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperCarga.Application.Domain.Finances.Model;

namespace SuperCarga.Persistence.Database.EntityConfigurations
{
    public class PaymentsEntityConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("payments", SuperCargaContext.DEFAULT_SCHEMA);

            builder.HasKey(i => i.Id).HasName("PK_payments");

            builder.Property(i => i.Id).HasColumnName("id");
            builder.Property(i => i.Created).HasColumnName("created");

            builder.Property(i => i.Operation).HasColumnName("operation");
            builder.Property(i => i.OperationValue).HasColumnName("operation_value");
            builder.Property(i => i.FromUserBalanceBefore).HasColumnName("from_user_balance_before");
            builder.Property(i => i.FromUserBalanceAfter).HasColumnName("from_user_balance_after");
            builder.Property(i => i.FromUserId).HasColumnName("from_user_id");
            builder.Property(i => i.ToUserBalanceBefore).HasColumnName("to_user_balance_before");
            builder.Property(i => i.ToUserBalanceAfter).HasColumnName("to_user_balance_after");
            builder.Property(i => i.ToUserId).HasColumnName("to_user_id");
            builder.Property(i => i.RelatedContractId).HasColumnName("related_contract_id");
        }
    }
}
