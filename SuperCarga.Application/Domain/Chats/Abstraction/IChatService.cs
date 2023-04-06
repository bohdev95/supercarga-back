using SuperCarga.Application.Domain.Chats.Queries.GetChat;
using SuperCarga.Application.Domain.Chats.Model;
using SuperCarga.Application.Domain.Chats.Commands;

namespace SuperCarga.Application.Domain.Chats.Abstraction
{
    public interface IChatService
    {
        Task<List<UserChatsDto>> GetUserChatsAsync(Guid userId, DateTime? lastDateTime);
        Task<List<Chat>> GetUserToUserChatsAsync(Guid fromUserId, Guid toUserId, DateTime? lastDateTime);
        Task<CreateChatResponse> CreateChatMessage(CreateChatCommand command, CancellationToken cancellationToken);
    }
}
