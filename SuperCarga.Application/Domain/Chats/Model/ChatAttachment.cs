namespace SuperCarga.Application.Domain.Chats.Model
{
    public class ChatAttachment
    {
        public Guid Id { get; set; }
        public Guid ChatId { get; set; }
        public string FileName { get; set; }
        public byte[] FileData { get; set; }
    }
}
