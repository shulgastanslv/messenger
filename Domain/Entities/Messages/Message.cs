using Domain.Primitives;

namespace Domain.Entities.Messages;

public class Message : Entity
{
    public Message(Guid id, string content, Guid receiver)
        : base(id)
    {
        Id = id;
        Content = content;
        Receiver = receiver;
        SendTime = DateTime.Now;
    }

    public Guid Receiver { get; set; }
    public DateTime SendTime { get; set; }
    public string Content { get; set; }
}