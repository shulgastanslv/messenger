using System;
using Domain.Entities.Users;

namespace Client.Models;

public class UserModel : User
{
    public UserModel(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public UserModel(){}

    public string Email { get; set; }

    public string Password { get; set; }
}
