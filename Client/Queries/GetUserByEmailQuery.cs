using Client.Commands;
using Client.Models;
using Client.Stores;
using Client.ViewModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Domain.Entities.Users;

namespace Client.Queries;

public sealed class GetUserByEmailQuery : ViewModelCommand
{
    private readonly HomeViewModel _homeViewModel;

    private readonly UserStore _userStore;

    private readonly HttpClient _httpClient;

    public GetUserByEmailQuery(HomeViewModel homeViewModel, UserStore userStore, HttpClient httpClient)
    {
        _userStore = userStore;
        _httpClient = httpClient;
        _homeViewModel = homeViewModel;

        Execute(null);
    }

    public sealed override async void Execute(object? parameter)
    {
        _homeViewModel.IsLoading = true;

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
           _userStore.Token);

        var response = await _httpClient.GetAsync($"https://localhost:7289/users/getUserByEmail?email={_userStore.User.Email}");

        if (response.IsSuccessStatusCode)
        {
            _userStore.User = await response.Content.ReadAsAsync<UserModel>();

            _homeViewModel.IsLoading = false;
        }
    }
}