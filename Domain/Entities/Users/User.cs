using Domain.Entities.Chats;
using Domain.Primitives;

namespace Domain.Entities.Users;

public class User : Entity
{
    public User(Guid id, string userName, string password)
        : base(id)
    {
        UserName = userName;
        Password = password;
        CreationTime = DateTime.Now;
    }

    public User() { }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public DateTime CreationTime { get; set; }

}