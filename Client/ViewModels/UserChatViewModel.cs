using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Client.Interfaces;
using Client.Models;
using Microsoft.IdentityModel.Tokens;

namespace Client.ViewModels;

public class UserChatViewModel : ViewModel
{
    private ObservableCollection<MessageViewModel> _messages;

    private string _message;

    private UserModel _user;

    public ObservableCollection<MessageViewModel> Messages
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
        Messages = new ObservableCollection<MessageViewModel>();
    }

    private bool CanExecuteSendMessageCommand(object obj)
    {
        if (Message.IsNullOrEmpty())
            return false;

        return true;
    }

    private void ExecuteSendMessageCommand(object obj)
    {
        var message = new MessageModel()
        {
            Id = Guid.NewGuid(),
            From = "test1",
            To = "test2",
            Content = Message,
            SendTime = DateTime.Now
        };

        var messageViewModel = new MessageViewModel(message);

        Messages.Add(messageViewModel);

        Message = string.Empty;
    }
}