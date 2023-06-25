using Client.Properties;
using System;
using System.IO;

namespace Client.Models;

public class ContactModel : EntityModel
{
    public ContactModel(Guid id, string username, Guid? chatId)
        : base(id)
    {
        Username = username;
        ChatId = chatId;
    }

    public string Username { get; set; }
    public string Avatar => Path.Combine(Settings.Default.AvatarsDataPath, Id.ToString().ToUpper() + ".png");
    public Guid? ChatId { get; set; }
}