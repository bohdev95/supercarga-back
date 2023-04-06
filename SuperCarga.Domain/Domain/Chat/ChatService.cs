using Microsoft.EntityFrameworkCore;
using SuperCarga.Application.Domain.Chats.Dto;
using SuperCarga.Application.Domain.Chats.Model;
using SuperCarga.Application.Domain.Chats.Abstraction;
using SuperCarga.Persistence.Database;
using SuperCarga.Application.Domain.Chats.Commands.CreateMessage;

namespace SuperCarga.Domain.Domain.Services
{
    public class ChatService : IChatService
    {
        private readonly SuperCargaContext _superCargaContext;
        public ChatService(SuperCargaContext superCargcontext)
        {
            _superCargaContext= superCargcontext;
        }
        
        public async Task<List<UserChatsDto>> GetUserChatsAsync(Guid userId, DateTime? lastDateTime){


            var chatsQuery = this._superCargaContext.Chats.AsNoTracking()
                    .Where(c => c.FromUserId == userId && c.DeletedDateTime == null);


            if (lastDateTime.HasValue)
            {
                chatsQuery = chatsQuery.Where(c => c.UpdatedDateTime > lastDateTime.Value);
            }

            return await chatsQuery.GroupBy(groupBy => groupBy.ToUserId)
                .Select(userchats => new UserChatsDto
                {
                    ToUserId = userchats.Key,
                    LastChatMessage = userchats.OrderByDescending(c => c.UpdatedDateTime)
                        .Single().ChatMessage,
                    LastUpdatedDateTime = userchats.Max(c => c.UpdatedDateTime),
                    UnreadMessages = userchats.Count(c => c.MessageReadDateTime == null)
                }).OrderByDescending(c=> c.LastUpdatedDateTime).ToListAsync();
        }

        public async Task<List<Chat>> GetUserToUserChatsAsync(Guid fromUserId, Guid toUserId, DateTime? lastDateTime) {

            var chatsQuery = this._superCargaContext.Chats.AsNoTracking()
                .Where(c => c.FromUserId == fromUserId && c.ToUserId == toUserId && c.DeletedDateTime == null);

            if (lastDateTime.HasValue)
            {
                chatsQuery = chatsQuery.Where(c => c.UpdatedDateTime > lastDateTime.Value);
            }

            return await chatsQuery.OrderByDescending(c=> c.UpdatedDateTime).ToListAsync();

        }

        //add chat
        public async Task<CreateMessageCommandResponse> CreateChatMessage(CreateMessageCommand command, CancellationToken cancellationToken) {

            var id = Guid.NewGuid();

            var message = new Chat { 
                Id = id,
                FromUserId = command.FromUserId,
                ToUserId = command.ToUserId,
                ChatMessage = command.ChatMessage,
                Created = DateTime.Now,
                UpdatedDateTime= DateTime.Now,
            };

            await this._superCargaContext.Chats.AddAsync(message);
            await this._superCargaContext.SaveChangesAsync(cancellationToken);

            return new CreateMessageCommandResponse { Id = id };
        }

        //add chat with chatattachment

    }
}
