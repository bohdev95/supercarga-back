using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperCarga.Application.Domain.Contracts.Common.Models;

namespace SuperCarga.Persistence.Database.EntityConfigurations
{
    public class ContractAdditionalCostEntityConfiguration : IEntityTypeConfiguration<ContractAdditionalCost>
    {
        public void Configure(EntityTypeBuilder<ContractAdditionalCost> builder)
        {
            builder.ToTable("contract_additional_costs", SuperCargaContext.DEFAULT_SCHEMA);

            builder.HasKey(i => i.Id).HasName("PK_contract_additional_costs");

            builder.Property(i => i.Id).HasColumnName("id");
            builder.Property(i => i.Created).HasColumnName("created");
            
            builder.Property(i => i.ContractId).HasColumnName("contract_id");
            builder.Property(i => i.Name).HasColumnName("name");
            builder.Property(i => i.Price).HasColumnName("price");
        }
    }
}
