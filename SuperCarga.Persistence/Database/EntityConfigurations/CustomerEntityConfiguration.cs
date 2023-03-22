using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperCarga.Application.Domain.Customers.Common.Models;
using SuperCarga.Application.Domain.Users.Models;

namespace SuperCarga.Persistence.Database.EntityConfigurations
{
    public class CustomerEntityConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("customers", SuperCargaContext.DEFAULT_SCHEMA);

            builder.HasKey(i => i.Id).HasName("PK_customers");

            builder.Property(i => i.Id).HasColumnName("id");
            builder.Property(i => i.Created).HasColumnName("created");

            builder.Property(i => i.IdDocumentPath).HasColumnName("id_document_path");

            builder.HasOne(i => i.User).WithOne().HasForeignKey<User>(c => c.CustomerId);

            builder
                .HasMany(x => x.FavoriteProposals)
                .WithMany(x => x.AddedToFavoriteBy)
                .UsingEntity<CustomerFavoriteProposal>(
                    x =>
                        x.HasOne(x => x.Proposal)
                        .WithMany()
                        .HasForeignKey(x => x.ProposalId),
                    x =>
                        x.HasOne(x => x.Customer)
                        .WithMany()
                        .HasForeignKey(x => x.CustomerId));
        }
    }
}
