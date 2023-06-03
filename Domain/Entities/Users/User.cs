using Domain.Primitives;

namespace Domain.Entities.Users;

public class User : Entity
{
    public User(Guid id, string userName, string email, string password)
        : base(id)
    {
        Id = id;
        UserName = userName;
        Email = email;
        Password = password;
        CreationTime = DateTime.Now;
    }

    public User() { }

    public Guid Id { get; set; }

    public string? UserName { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public DateTime CreationTime { get; set; }

}