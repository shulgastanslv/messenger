using Domain.Entities;

namespace Client.Models;

public class MessageModel : Message
{
    public string? UserColor { get; set; }
    public string? ImageSource { get; set; }
    public bool IsNativeOrigin { get; set; }
    public bool ? FirstMessage { get; set; }
}