using System;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using Client.Models;
using Client.ViewModels;
using Newtonsoft.Json;

namespace Client.Commands.Messages;

public class SendMessageCommand : ViewModelCommand
{
    private readonly ContactModel _receiver;

    private readonly HttpClient _httpClient;

    private readonly ChatViewModel _chatViewModel;

    public SendMessageCommand(HttpClient httpClient, ChatViewModel chatViewModel, ContactModel receiver)
    {
        _httpClient = httpClient;
        _chatViewModel = chatViewModel;
        _receiver = receiver;

        _chatViewModel.PropertyChanged += OnPropertyChanged;
    }

    private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ChatViewModel.MessageText))
        {
            OnCanExecutedChanged();
        }
    }

    public override bool CanExecute(object? parameter) =>
        !string.IsNullOrEmpty(_chatViewModel.MessageText);

    public override async void Execute(object? parameter)
    {
        var message = new MessageModel(
            Guid.NewGuid(),
            _chatViewModel.MessageText,
            _receiver.Id);

        var content = new StringContent(JsonConvert.SerializeObject(message),
            Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("/receiver", content);

        response.EnsureSuccessStatusCode();
    }
}