using System.Collections.Generic;
using System.ComponentModel;
using Client.Services;
using Client.Stores;
using Client.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Net.Http;
using System.Windows.Navigation;
using Client.Models;
using System.Net.Http.Headers;

namespace Client.Commands;

public class GetAllUsersCommand : ViewModelCommand
{
    private readonly HomeViewModel _homeViewModel;

    private readonly HttpClient _httpClient;

    private readonly UserStore _userStore;

    public GetAllUsersCommand(UserStore userStore, HomeViewModel homeViewModel,
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