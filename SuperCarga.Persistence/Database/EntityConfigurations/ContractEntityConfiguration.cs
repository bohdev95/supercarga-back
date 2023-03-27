using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperCarga.Application.Domain.Contracts.Common.Models;

namespace SuperCarga.Persistence.Database.EntityConfigurations
{
    public class ContractEntityConfiguration : IEntityTypeConfiguration<Contract>
    {
        public void Configure(EntityTypeBuilder<Contract> builder)
        {
            builder.ToTable("contracts", SuperCargaContext.DEFAULT_SCHEMA);

            builder.HasKey(i => i.Id).HasName("PK_contracts");

            builder.Property(i => i.Id).HasColumnName("id");
            builder.Property(i => i.Created).HasColumnName("created");
            
            builder.Property(i => i.ProposalId).HasColumnName("proposal_id");
            builder.Property(i => i.JobId).HasColumnName("job_id");
            builder.Property(i => i.DriverId).HasColumnName("driver_id");
            builder.Property(i => i.CustomerId).HasColumnName("customer_id");
            builder.Property(i => i.State).HasColumnName("state");
            builder.Property(i => i.PaymentState).HasColumnName("payment_state");
            builder.Property(i => i.PricePerKm).HasColumnName("price_per_km");
            builder.Property(i => i.PricePerDistance).HasColumnName("price_per_distance");
            builder.Property(i => i.TotalPrice).HasColumnName("total_price");
            builder.Property(i => i.ServiceFee).HasColumnName("service_fee");
            builder.Property(i => i.Price).HasColumnName("price");
            builder.Property(i => i.PickUpCargoImagePath).HasColumnName("pick_up_cargo_image_path");
            builder.Property(i => i.PickUpProofImagePath).HasColumnName("pick_up_proof_image_path");
            builder.Property(i => i.DeliveryCargoImagePath).HasColumnName("delivery_cargo_image_path");
            builder.Property(i => i.DeliveryProofImagePath).HasColumnName("delivery_proof_image_path");
            builder.Property(i => i.Rating).HasColumnName("rating");
            builder.Property(i => i.RatingComment).HasColumnName("rating_comment");

            builder.HasOne(i => i.Proposal).WithOne().HasForeignKey<Contract>(i => i.ProposalId);
            builder.HasOne(i => i.Job).WithMany(j => j.Contracts).HasForeignKey(i => i.JobId);
            builder.HasMany(i => i.History).WithOne().HasForeignKey(c => c.ContractId);
            builder.HasMany(i => i.Additions).WithOne().HasForeignKey(c => c.ContractId);
            builder.HasOne(i => i.Driver).WithMany().HasForeignKey(i => i.DriverId);
            builder.HasOne(i => i.Customer).WithMany().HasForeignKey(c => c.CustomerId);
        }
    }
}
