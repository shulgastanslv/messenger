using Domain.Primitives;

namespace Domain.Entities.Messages;

public class Message : Entity
{
    public Message()
    {
    }

    public Message(Guid id, string content, Guid sender, Guid receiverChatId)
        : base(id)
    {
        Content = content;
        SendTime = DateTime.Now;
        Sender = sender;
        ReceiverChatId = receiverChatId;
    }

    public Message(Guid id, Media media, Guid sender, Guid receiverChatId)
        : base(id)
    {
        Media = media;
        SendTime = DateTime.Now;
        Sender = sender;
        ReceiverChatId = receiverChatId;
    }

    public string Content { get; set; }

    public DateTime SendTime { get; set; }

    public Media Media { get; set; }

    public bool HasMedia => Media != null;

    public Guid Sender { get; set; }

    public Guid ReceiverChatId { get; set; }
}