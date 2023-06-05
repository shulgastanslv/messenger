using Client.Models;

namespace Client.ViewModels;

public class MessageViewModel : ViewModelBase
{
    public MessageModel Message { get; set; }

    public MessageViewModel(MessageModel message)
    {
        Message = message;
    }
}