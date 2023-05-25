using Domain.Entities.User;

namespace Client.Models;

public class UserModel : User
{
    public string ImageSource { get; set; }
    public string LastMessage { get; set; }
}
