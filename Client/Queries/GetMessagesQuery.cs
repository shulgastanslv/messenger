using Client.Commands;
using Client.Stores;
using Client.ViewModels;
using System.Net.Http;
using Client.Models;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Newtonsoft.Json;
using System.Text;

namespace Client.Queries;

public sealed class GetMessagesQuery : ViewModelCommand
{

    private readonly ChatViewModel _chatViewModel;
    private readonly HttpClient _httpClient;

    public GetMessagesQuery(ChatViewModel chatViewModel, HttpClient httpClient)
    {
        _chatViewModel = chatViewModel;
        _httpClient = httpClient;
    }

    public override async void Execute(object? parameter)
    {
        var content = new StringContent(
            JsonConvert.SerializeObject(_chatViewModel.CurrentContact),
            Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("/message/get", content);

        if (!response.IsSuccessStatusCode)
        {
            return;
        }

        var messages = await response.Content
            .ReadAsAsync<List<MessageModel>>();

        _chatViewModel.Messages = new ObservableCollection<MessageModel>(
            messages.OrderBy(i => i.SendTime));
    }
}