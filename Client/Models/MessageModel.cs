using System;

namespace Client.Models;

public class MessageModel : EntityModel
{
    public MessageModel(Guid id, string content, MediaModel media,
        Guid sender, Guid receiverChatId)
        : base(id)
    {
        Content = content;
        Media = media;
        Sender = sender;
        ReceiverChatId = receiverChatId;
        SendTime = DateTime.Now;
    }

    public MessageModel()
    {
    }

    public MediaModel Media { get; set; }

    public bool HasMedia => Media != null;

    public string Content { get; set; }

    public Guid Sender { get; set; }

    public Guid ReceiverChatId { get; set; }

    public DateTime SendTime { get; set; }
}