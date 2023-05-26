using Client.Models;
using Domain.Entities;

namespace Client.ViewModels;

public class MessageViewModel : ViewModel
{
    private MessageModel _message;

    public MessageModel Message
    {
        get => _message;
        set
        {
            _message = value;
            OnPropertyChanged(nameof(Message));
        }
    }
    public MessageViewModel(MessageModel message)
    {
        Message = message;
    }
}