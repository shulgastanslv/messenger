using System;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading;
using Client.Models;
using Client.Queries;
using Client.Services;
using Client.ViewModels;
using Newtonsoft.Json;

namespace Client.Commands.Messages;

public class SendMessageCommand : CommandBase
{
    private readonly ChatViewModel _chatViewModel;

    private readonly ContactModel _contactReceiver;

    private readonly HttpClient _httpClient;

    public SendMessageCommand(ChatViewModel chatViewModel, ContactModel contactReceiver, HttpClient httpClient)
    {
        _chatViewModel = chatViewModel;
        _contactReceiver = contactReceiver;
        _httpClient = httpClient;

        _chatViewModel.PropertyChanged += OnPropertyChanged;
    }

    private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ChatViewModel.MessageText))
            OnCanExecutedChanged();
    }

    public override bool CanExecute(object? parameter)
    {
        return !string.IsNullOrEmpty(_chatViewModel.MessageText);
    }

    public override async void Execute(object? parameter)
    {
        _contactReceiver.ChatId ??=
            await ChatService.CreateChatAsync(_httpClient, _contactReceiver, CancellationToken.None);

        var message = new MessageModel(
            Guid.NewGuid(),
            _chatViewModel.MessageText,
            null,
            Guid.Empty,
            _contactReceiver.ChatId.Value);

        var content = new StringContent(JsonConvert.SerializeObject(message),
            Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("/message/send", content);

        response.EnsureSuccessStatusCode();

        _chatViewModel.MessageText = string.Empty;
    }
}