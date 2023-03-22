using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperCarga.Application.Domain.Contracts.Common.Models;

namespace SuperCarga.Persistence.Database.EntityConfigurations
{
    public class ContractHistoryEntityConfiguration : IEntityTypeConfiguration<ContractHistory>
    {
        public void Configure(EntityTypeBuilder<ContractHistory> builder)
        {
            builder.ToTable("contract_histories", SuperCargaContext.DEFAULT_SCHEMA);

            builder.HasKey(i => i.Id).HasName("PK_contract_histories");

            builder.Property(i => i.Id).HasColumnName("id");
            builder.Property(i => i.Created).HasColumnName("created");
            
            builder.Property(i => i.ContractId).HasColumnName("contract_id");
            builder.Property(i => i.State).HasColumnName("state");
        }
    }
}
