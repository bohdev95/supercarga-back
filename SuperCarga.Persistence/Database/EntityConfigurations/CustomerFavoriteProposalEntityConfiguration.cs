using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperCarga.Application.Domain.Customers.Common.Models;
using SuperCarga.Application.Domain.Drivers.Common.Models;

namespace SuperCarga.Persistence.Database.EntityConfigurations
{
    public class CustomerFavoriteProposalEntityConfiguration : IEntityTypeConfiguration<CustomerFavoriteProposal>
    {
        public void Configure(EntityTypeBuilder<CustomerFavoriteProposal> builder)
        {
            builder.ToTable("customer_favorite_proposals", SuperCargaContext.DEFAULT_SCHEMA);

            builder.HasKey(i => new { i.CustomerId, i.ProposalId }).HasName("PK_customer_favorite_proposals");

            builder.Property(i => i.CustomerId).HasColumnName("customer_id");
            builder.Property(i => i.ProposalId).HasColumnName("proposal_id");
        }
    }
}
