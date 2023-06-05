using System;
using System.Collections.Generic;
using Client.Models;
using Client.Stores;
using System.Net.Http;
using System.Windows.Input;
using Client.Commands;
using Client.Services;
using System.Collections.ObjectModel;
using System.Windows;
using MediatR;

namespace Client.ViewModels;

public class ChatViewModel : ViewModelBase
{
    private UserModel? _userModel;
    public UserModel? UserModel
    {
        get => _userModel;
        set
        {
            _userModel = value;
            OnPropertyChanged(nameof(UserModel));
        }
    }

    private ObservableCollection<MessageModel> _messages = new ();

    public ObservableCollection<MessageModel> Messages
    {
        get => _messages;
        set
        {
            _messages = value;
            OnPropertyChanged(nameof(Messages));
        }
    }

    private string _message;

    public string Message
    {
        get => _message;
        set
        {
            _message = value;
            OnPropertyChanged(nameof(Message));
        }
    }

    public ICommand SendMessageCommand { get; }

    public ChatViewModel(UserModel? userModel, UserStore userStore)
    {
        _userModel = userModel;

        SendMessageCommand = new SendMessageCommand(userModel, userStore, Messages, this);
    }

}