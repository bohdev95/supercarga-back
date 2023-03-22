using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperCarga.Application.Domain.Customers.Common.Models;
using SuperCarga.Application.Domain.Proposals.Common.Models;

namespace SuperCarga.Persistence.Database.EntityConfigurations
{
    public class ProposalEntityConfiguration : IEntityTypeConfiguration<Proposal>
    {
        public void Configure(EntityTypeBuilder<Proposal> builder)
        {
            builder.ToTable("proposals", SuperCargaContext.DEFAULT_SCHEMA);

            builder.HasKey(i => i.Id).HasName("PK_proposals");

            builder.Property(i => i.Id).HasColumnName("id");
            builder.Property(i => i.Created).HasColumnName("created");

            builder.Property(i => i.JobId).HasColumnName("job_id");
            builder.Property(i => i.DriverId).HasColumnName("driver_id");
            builder.Property(i => i.PricePerKm).HasColumnName("price_per_km");
            builder.Property(i => i.State).HasColumnName("state");
            builder.Property(i => i.Checked).HasColumnName("checked");

            builder.HasOne(i => i.Driver).WithMany().HasForeignKey(i => i.DriverId);
            builder.HasOne(i => i.Job).WithMany(x => x.Proposals).HasForeignKey(i => i.JobId);

            builder
                .HasMany(x => x.AddedToFavoriteBy)
                .WithMany(x => x.FavoriteProposals)
                .UsingEntity<CustomerFavoriteProposal>(
                    x =>
                        x.HasOne(x => x.Customer)
                        .WithMany()
                        .HasForeignKey(x => x.CustomerId),
                    x =>
                        x.HasOne(x => x.Proposal)
                        .WithMany()
                        .HasForeignKey(x => x.ProposalId));
        }
    }
}
