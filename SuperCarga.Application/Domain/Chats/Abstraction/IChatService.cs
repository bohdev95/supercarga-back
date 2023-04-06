using SuperCarga.Application.Domain.Chats.Dto;
using SuperCarga.Application.Domain.Chats.Model;
using SuperCarga.Application.Domain.Chats.Commands.CreateMessage;

namespace SuperCarga.Application.Domain.Chats.Abstraction
{
    public interface IChatService
    {
        Task<List<UserChatsDto>> GetUserChatsAsync(Guid userId, DateTime? lastDateTime);
        Task<List<Chat>> GetUserToUserChatsAsync(Guid fromUserId, Guid toUserId, DateTime? lastDateTime);
        Task<CreateMessageCommandResponse> CreateChatMessage(CreateMessageCommand command, CancellationToken cancellationToken);
    }
}
