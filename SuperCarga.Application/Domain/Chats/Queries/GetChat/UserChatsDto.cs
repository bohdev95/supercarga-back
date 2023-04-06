namespace SuperCarga.Application.Domain.Chats.Queries.GetChat
{
    public class UserChatsDto
    {
        public Guid ToUserId { get; set; }
        public string LastChatMessage { get; set; }
        public DateTime LastUpdatedDateTime { get; set; }

        public int? UnreadMessages { get; set; }
    }
}
