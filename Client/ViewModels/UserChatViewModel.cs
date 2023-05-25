using System.Collections.ObjectModel;
using System.Windows.Input;
using Client.Interfaces;
using Client.Models;

namespace Client.ViewModels;

public class UserChatViewModel : ViewModel
{
    private ObservableCollection<MessageModel> _messages;

    private string _message;

    private UserModel _user;

    public ObservableCollection<MessageModel> Messages
    {
        get => _messages;
        set
        {
            _messages = value;
            OnPropertyChanged(nameof(Messages));
        }
    }

    public string Message
    {
        get => _message;
        set
        {
            _message = value;
            OnPropertyChanged(nameof(Message));
        }
    }

    private INavigationService _navigationService;

    public UserModel User
    {
        get => _user;
        set
        {
            _user = value;
            OnPropertyChanged(nameof(User));
        }
    }

    public INavigationService NavigationService
    {
        get => _navigationService;
        set
        {
            _navigationService = value;
            OnPropertyChanged();
        }
    }

    public ICommand SendMessageCommand { get; }

    public UserChatViewModel(INavigationService navigationService)
    {
        NavigationService = navigationService;
        SendMessageCommand = new ViewModelCommand(ExecuteSendMessageCommand, CanExecuteSendMessageCommand);
    }

    private bool CanExecuteSendMessageCommand(object obj)
    {
        return true;
    }

    private void ExecuteSendMessageCommand(object obj)
    {
        var message = new MessageModel()
        {
            Content = Message
        };

        Messages.Add(message);
    }
}