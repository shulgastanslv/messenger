namespace Domain.Entities;

public class User
{
    public string Id { get; set; }

    public string UserName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public DateTime CreationTime { get; set; }

}