namespace Domain.Entities;

public class Message
{
    public string Id { get; set; }
    public User Sender { get; set; }
    public User Recipient { get; set; }
    public string Text { get; set; }
    public DateTime SentTime { get; set; }
}