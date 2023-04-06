using SuperCarga.Application.Domain.Common.Model;
using SuperCarga.Application.Domain.Users.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Chats.Model
{
    public class Chat : Entity
    {
        public Guid FromUserId { get; set; }
        public Guid ToUserId { get; set; }
        public string ChatMessage { get; set; }
        public bool HasAttachment { get; set; }
        public DateTime? MessageReadDateTime { get; set; }
        public DateTime? DeletedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public User? FromUser { get; set; }
    }
}
