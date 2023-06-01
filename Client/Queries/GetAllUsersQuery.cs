using System.Collections.Generic;
using Client.Stores;
using Client.ViewModels;
using System.Net.Http;
using Client.Models;
using System.Net.Http.Headers;
using Client.Commands;

namespace Client.Queries;

public class GetAllUsersQuery : ViewModelCommand
{
    private readonly HomeViewModel _homeViewModel;

    private readonly HttpClient _httpClient;

    private readonly UserStore _userStore;

    public GetAllUsersQuery(HomeViewModel homeViewModel, UserStore userStore,
        HttpClient httpClient)
    {
        _homeViewModel = homeViewModel;
        _httpClient = httpClient;
        _userStore = userStore;
    }

    public sealed override async void Execute(object? parameter)
    {
        _homeViewModel.IsLoading = true;

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
            _userStore.Token);

        var response = await _httpClient.GetAsync("https://localhost:7289/users/getAllUsers");

        if (response.IsSuccessStatusCode)
        {
            _homeViewModel.Users = await response.Content.ReadAsAsync<List<UserModel>>();

            _homeViewModel.IsLoading = false;
        }
    }
}