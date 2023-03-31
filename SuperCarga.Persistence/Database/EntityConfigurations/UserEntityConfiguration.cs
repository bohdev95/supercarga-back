using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperCarga.Application.Domain.Finances.Model;
using SuperCarga.Application.Domain.Users.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Persistence.Database.EntityConfigurations
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users", SuperCargaContext.DEFAULT_SCHEMA);

            builder.HasKey(i => i.Id).HasName("PK_users");

            builder.Property(i => i.Id).HasColumnName("id");
            builder.Property(i => i.Created).HasColumnName("created");
            builder.Property(i => i.Email).HasColumnName("email");
            builder.Property(i => i.EmailNormalized).HasColumnName("email_normalized");
            builder.Property(i => i.FirstName).HasColumnName("first_name");
            builder.Property(i => i.LastName).HasColumnName("last_name");
            builder.Property(i => i.CustomerId).HasColumnName("customer_id");
            builder.Property(i => i.DriverId).HasColumnName("driver_id");
            builder.Property(i => i.Password).HasColumnName("password");
            builder.Property(i => i.IsActive).HasColumnName("is_active");
            builder.Property(i => i.TermsAccepted).HasColumnName("terms_accepted");
            builder.Property(i => i.RefreshToken).HasColumnName("refresh_token");
            builder.Property(i => i.RefreshTokenExpiry).HasColumnName("refresh_token_expiry");
            builder.Property(i => i.ImagePath).HasColumnName("image_path");
            builder.Property(i => i.VerificationState).HasColumnName("verification_state");

            builder.HasOne(i => i.Finance).WithOne().HasForeignKey<Finance>(i => i.UserId);
            builder.HasMany(i => i.FromPayments).WithOne().HasForeignKey(i => i.FromUserId);
            builder.HasMany(i => i.ToPayments).WithOne().HasForeignKey(i => i.ToUserId);

            builder
                .HasMany(x => x.Roles)
                .WithMany(x => x.Users)
                .UsingEntity<UserRole>(
                    x =>
                        x.HasOne(x => x.Role)
                        .WithMany()
                        .HasForeignKey(x => x.RoleId),
                    x =>
                        x.HasOne(x => x.User)
                        .WithMany()
                        .HasForeignKey(x => x.UserId));
        }
    }
}
