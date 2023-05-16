namespace Domain.Entities;

public class Contact
{
    public string Id { get; set; }
    public User Owner { get; set; }
    public User ContactUser { get; set; }
}