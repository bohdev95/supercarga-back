using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SuperCarga.Application.Domain.Chats.Abstraction;

namespace SuperCarga.Application.Domain.Chats.Commands
{
    public class CreateChatCommand : IRequest<CreateChatResponse>
    {
        public Guid FromUserId { get; set; }
        public Guid ToUserId { get; set; }
        public string ChatMessage { get; set; }

        //public string? FileName { get; set;}
        //public Byte[]? FileData { get; set; }
    }


    public class CreateChatCommandHandler: IRequestHandler<CreateChatCommand, CreateChatResponse> { 
    
        IChatService _chatService;

        public CreateChatCommandHandler(IChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task<CreateChatResponse> Handle(CreateChatCommand command, CancellationToken token) {
            return await this._chatService.CreateChatMessage(command, token);
        }
    }
}
