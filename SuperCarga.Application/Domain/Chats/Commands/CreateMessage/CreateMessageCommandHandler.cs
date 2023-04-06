using MediatR;
using SuperCarga.Application.Domain.Chats.Abstraction;

namespace SuperCarga.Application.Domain.Chats.Commands.CreateMessage
{
    public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, CreateMessageCommandResponse>
    {

        private readonly IChatService _chatService;

        public CreateMessageCommandHandler(IChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task<CreateMessageCommandResponse> Handle(CreateMessageCommand command, CancellationToken token)
        {
            return await _chatService.CreateChatMessage(command, token);
        }
    }
}
