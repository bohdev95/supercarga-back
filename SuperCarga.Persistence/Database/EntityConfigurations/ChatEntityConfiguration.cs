using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperCarga.Application.Domain.Jobs.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperCarga.Application.Domain.Chats.Model;
using SuperCarga.Application.Domain.Users.Models;

namespace SuperCarga.Persistence.Database.EntityConfigurations
{
    public class ChatEntityConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder) {

            builder.ToTable("chats", SuperCargaContext.DEFAULT_SCHEMA);
            builder.HasKey(i=> i.Id);
            builder.Property(i => i.Id).HasColumnName("id");
            builder.Property(i => i.Created).HasColumnName("created");
            builder.Property(i => i.FromUserId).HasColumnName("from_user_id");
            builder.Property(i => i.ToUserId).HasColumnName("to_user_id");
            builder.Property(i => i.ChatMessage).HasColumnName("chat_message");
            builder.Property(i => i.HasAttachment).HasColumnName("has_attachment");
            builder.Property(i => i.MessageReadDateTime).HasColumnName("message_read_datetime");
            builder.Property(i => i.DeletedDateTime).HasColumnName("deleted_datetime");
            builder.Property(i => i.UpdatedDateTime).HasColumnName("updated_datetime");


            builder.HasOne(i => i.FromUser).WithOne().HasForeignKey<Chat>(i => i.FromUserId);

        }
    }

    public class ChatAttachmentsEntityConfiguration : IEntityTypeConfiguration<ChatAttachment>
    {
        public void Configure(EntityTypeBuilder<ChatAttachment> builder)
        {

            builder.ToTable("chat_attachments", SuperCargaContext.DEFAULT_SCHEMA);
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id).HasColumnName("id");
            builder.Property(i => i.ChatId).HasColumnName("chat_id");
            builder.Property(i => i.FileName).HasColumnName("file_name");
            builder.Property(i => i.FileData).HasColumnName("file_data");
        }
    }
}
