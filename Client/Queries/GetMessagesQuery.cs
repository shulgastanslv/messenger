using Client.Commands;
using Client.Stores;
using Client.ViewModels;
using System.Net.Http;
using Client.Models;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Client.Queries;

public sealed class GetMessagesQuery : ViewModelCommand
{
    private readonly ChatViewModel _chatViewModel;

    private readonly HttpClient _httpClient;

    private readonly UserStore _sender;

    private readonly UserModel _receiver;

    public GetMessagesQuery(UserStore sender, UserModel receiver, ChatViewModel chatViewModel, HttpClient httpClient)
    {
        _chatViewModel = chatViewModel;
        _httpClient = httpClient;
        _sender = sender;
        _receiver = receiver;
    }

    public override async void Execute(object? parameter)
    {
        var response = await _httpClient.GetAsync($"getMessages?receiver={_receiver.UserName!}?sender={_sender.User.UserName!}");

        if (response.IsSuccessStatusCode)
        {
            _chatViewModel.Messages = await response.Content.ReadAsAsync<ObservableCollection<MessageModel>>();
        }

    }
}