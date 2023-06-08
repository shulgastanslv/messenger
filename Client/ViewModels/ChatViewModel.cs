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
using Client.Commands.Messages;
using Client.Queries;
using MediatR;

namespace Client.ViewModels;

public class ChatViewModel : ViewModelBase
{
    private readonly UserStore _userStore;
    public UserStore UserStore => _userStore;

    private ContactModel _currentContact;
    public ContactModel CurrentContact
    {
        get => _currentContact;
        set
        {
            _currentContact = value;
            OnPropertyChanged(nameof(CurrentContact));
        }
    }

    private ObservableCollection<MessageModel> _messages = new();
    public ObservableCollection<MessageModel> Messages
    {
        get => _messages;
        set
        {
            _messages = value;
            OnPropertyChanged(nameof(Messages));
        }
    }

    private string _messageText;
    public string MessageText
    {
        get => _messageText;
        set
        {
            _messageText = value;
            OnPropertyChanged(nameof(MessageText));
        }
    }
    public ICommand GetMessagesQuery { get; }
    public ICommand SendMessageCommand { get; }

    public ChatViewModel(UserStore userStore, ContactModel currentContact, HttpClient httpClient)
    {
        _currentContact = currentContact;
        _userStore = userStore;

        SendMessageCommand = new SendMessageCommand(httpClient, this, CurrentContact);
        GetMessagesQuery = new GetMessagesQuery(this, httpClient);
        GetMessagesQuery.Execute(null);
    }

}