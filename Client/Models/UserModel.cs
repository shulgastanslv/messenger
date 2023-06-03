using System;
using Domain.Entities.Users;

namespace Client.Models;

public class UserModel : User
{
    public UserModel(Guid id, string userName, string email, string password)
        : base(id, userName, email, password)
    {
        UserName = userName;
        Email = email;
        Password = password;
        CreationTime = DateTime.Now;
    }
    public UserModel() {}
}
