using System;

namespace Client.Models;

public class UserModel
{
    public UserModel(Guid id, string userName, string email, string password)
    {
        Id = id;
        UserName = userName;
        Email = email;
        Password = password;
        CreationTime = DateTime.Now;
    }
    public UserModel(){}

    public Guid Id { get; set; }

    public string UserName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public DateTime CreationTime { get; set; }
}
