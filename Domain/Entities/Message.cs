using Domain.Entities.Users;

namespace Domain.Entities;

public class Message
{
    public Guid Id { get; set; }
    public User Sender { get; set; }
    public User Receiver { get; set; }
    public DateTime SendTime { get; set; }
    public string Content { get; set; }
}