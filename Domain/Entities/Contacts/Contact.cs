using Domain.Primitives;

namespace Domain.Entities.Contacts;

public class Contact : Entity
{
    public Contact(Guid id, string username)
        : base(id)
    {
        Username = username;
    }

    public string Username { get; set; }
}