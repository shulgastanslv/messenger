using System;

namespace Client.Models;

public class UserModel : EntityModel
{
    public UserModel(Guid id, string username, string password) : base(id)
    {
        Username = username;
        Password = password;
        CreationTime = DateTime.Now;
    }

    public UserModel(){}

    public string Username { get; set; }
    public string Password { get; set; }
    public DateTime CreationTime { get; set; }
}