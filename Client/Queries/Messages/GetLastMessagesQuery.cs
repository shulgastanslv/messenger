using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using Client.Commands;
using Client.Models;
using Client.ViewModels;
using Newtonsoft.Json;

namespace Client.Queries.Messages;

public class GetLastMessagesQuery : ViewModelCommand
{
    private readonly ChatViewModel _chatViewModel;
    private readonly HttpClient _httpClient;

    public GetLastMessagesQuery(ChatViewModel chatViewModel, HttpClient httpClient)
    {
        _chatViewModel = chatViewModel;
        _httpClient = httpClient;
    }

    public override async void Execute(object? parameter)
    {
        var content = new StringContent(
            JsonConvert.SerializeObject(_chatViewModel.CurrentContact),
            Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("/message/getlast", content);

        if (!response.IsSuccessStatusCode)
        {
            return;
        }

        var messages = await response.Content
            .ReadAsAsync<IEnumerable<MessageModel>>();

        foreach (var message in messages)
        {
            _chatViewModel.Messages.Add(message);
        }
    }
}