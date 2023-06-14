﻿using Client.Models;
using Client.Services;
using Client.Stores;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System;
using Client.Commands;

namespace Client.Queries;

public class GetLastMessagesQuery : ViewModelCommand
{
    private readonly HttpClient _httpClient;
    private readonly UserStore _userStore;

    public GetLastMessagesQuery(HttpClient httpClient, UserStore userStore)
    {
        _httpClient = httpClient;
        _userStore = userStore;
    }

    public override async void Execute(object? parameter)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
            _userStore.Token);

        var response = await _httpClient.GetAsync($"/message/getlast"
                                                  + $"?LastResponseTime={_userStore.LastResponseTime:yyyy-MM-ddTHH:mm:ss}");

        _userStore.LastResponseTime = DateTime.Now;

        if (!response.IsSuccessStatusCode) return;

        var contacts = await response.Content
            .ReadAsAsync<IEnumerable<MessageModel>>();

        SaveEntityModelService.SaveMessages(contacts);
    }
}