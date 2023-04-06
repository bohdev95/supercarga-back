using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace SuperCarga.Application.Domain.Chats.Commands.CreateMessage
{
    public class CreateMessageCommand : IRequest<CreateMessageCommandResponse>
    {
        public Guid FromUserId { get; set; }
        public Guid ToUserId { get; set; }
        public string ChatMessage { get; set; }

        //public string? FileName { get; set;}
        //public Byte[]? FileData { get; set; }
    }
}
