using System;

namespace Client.Models;

public class ContactModel
{
    public ContactModel(Guid id, string userName)
    {
        Id = id;
        UserName = userName;
    }

    public Guid Id { get; set; }

    public string UserName { get; set; }
}