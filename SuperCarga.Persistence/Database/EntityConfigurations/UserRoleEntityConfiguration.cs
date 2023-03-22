using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperCarga.Application.Domain.Users.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Persistence.Database.EntityConfigurations
{
    public class UserRoleEntityConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("users_roles", SuperCargaContext.DEFAULT_SCHEMA);

            builder.HasKey(i => new { i.UserId, i.RoleId }).HasName("PK_users_roles");

            builder.Property(i => i.UserId).HasColumnName("user_id");
            builder.Property(i => i.RoleId).HasColumnName("role_id");
        }
    }
}
