using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Client.Models;
using Client.Stores;
using Client.ViewModels;

namespace Client.Commands;

public class SendMessageCommand : ViewModelCommand
{
    private readonly ChatViewModel _chatViewModel;

    private readonly UserModel _receiver;

    private readonly UserStore _sender;

    private readonly ObservableCollection<MessageModel> _messages;

    public SendMessageCommand(UserModel receiver, UserStore sender, ObservableCollection<MessageModel> messages, ChatViewModel chatViewModel)
    {
        _chatViewModel = chatViewModel;
        _receiver = receiver;
        _sender = sender;
        _messages = messages;

        chatViewModel.PropertyChanged += OnPropertyChanged;
    }

    private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ChatViewModel.Message))
        {
            OnCanExecutedChanged();
        }
    }

    public override bool CanExecute(object? parameter) =>
        !string.IsNullOrEmpty(_chatViewModel.Message);

    public override void Execute(object? parameter)
    {
        var message = new MessageModel
        {
            Id = Guid.NewGuid(),
            Receiver = _receiver,
            Sender = _sender.User,
            Content = _chatViewModel.Message,
            SendTime = DateTime.Now
        };

        _messages.Add(message);

        _chatViewModel.Message = string.Empty;
    }
}