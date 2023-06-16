using Client.Models;
using Client.Services;
using Client.ViewModels;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading;
using System;
using System.Linq;
using Client.Stores;

namespace Client.Commands.Messages;

public class LoadFilesCommand : CommandBase
{ 
    private readonly ChatViewModel _chatViewModel;
    private readonly HttpClient _httpClient;

    public LoadFilesCommand(ChatViewModel chatViewModel, HttpClient httpClient)
    {
        _chatViewModel = chatViewModel;
        _httpClient = httpClient;
    }

    public override async void Execute(object? parameter)
    {
        var messages =
            await MessageService.LoadLocalFilesAsync(_chatViewModel.CurrentContact, CancellationToken.None);

        if (messages == null || messages.Count == 0)
        {
            messages = await MessageService.SaveFilesAsync(
                _chatViewModel.CurrentContact, _httpClient, CancellationToken.None);
        }

        if (messages != null)
        {
            foreach (var item in messages)
            {
                if(!_chatViewModel.Messages.Contains(item))
                    _chatViewModel.Messages.Add(item);
            }
        }
    }
        
    public void Execute(object? parameter, EventArgs e)
    {
        Execute(parameter);
    }
}