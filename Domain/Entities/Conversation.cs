namespace Domain.Entities;

public class Conversation
{
    public string Id { get; set; }
    public List<User> Participants { get; set; }
    public List<Message> Messages { get; set; }
}