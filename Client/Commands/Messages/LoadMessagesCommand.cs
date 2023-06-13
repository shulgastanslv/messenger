using Client.Models;
using Client.ViewModels;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading;
using System;
using System.Linq;
using Client.Services;

namespace Client.Commands.Messages;

public class LoadMessagesCommand : ViewModelCommand
{
    private readonly ChatViewModel _chatViewModel;
    private readonly HttpClient _httpClient;

    public LoadMessagesCommand(ChatViewModel chatViewModel, HttpClient httpClient)
    {
        _chatViewModel = chatViewModel;
        _httpClient = httpClient;
    }

    public override async void Execute(object? parameter)
    {
        var messages =
            await MessageService.LoadLocalMessagesAsync(_chatViewModel.CurrentContact, CancellationToken.None);

        if (messages == null || messages.Count == 0)
        {
            messages = await MessageService.SaveMessagesAsync(
                _chatViewModel.CurrentContact, _httpClient, CancellationToken.None);
        }

        if (messages != null)
            _chatViewModel.Messages = new ObservableCollection<MessageModel>(messages.OrderBy(m => m.SendTime));
    }

    public void Execute(object? parameter, EventArgs e)
    {
        Execute(parameter);
    }
}