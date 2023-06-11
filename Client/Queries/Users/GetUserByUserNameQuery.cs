using System.Net.Http;
using System.Net.Http.Headers;
using Client.Commands;
using Client.Models;
using Client.Stores;
using Client.ViewModels;

namespace Client.Queries.Users;

public sealed class GetUserByUserNameQuery : ViewModelCommand
{
    private readonly HomeViewModel _homeViewModel;

    private readonly HttpClient _httpClient;

    private readonly UserStore _userStore;

    public GetUserByUserNameQuery(HomeViewModel homeViewModel, UserStore userStore, HttpClient httpClient)
    {
        _userStore = userStore;
        _httpClient = httpClient;
        _homeViewModel = homeViewModel;
    }

    public override async void Execute(object? parameter)
    {
        _homeViewModel.IsLoading = true;

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
            _userStore.Token);

        var response =
            await _httpClient.GetAsync(
                $"https://localhost:7289/users/getUserByUserName?UserName={_userStore.User.UserName}");

        if (response.IsSuccessStatusCode)
        {
            _userStore.User = await response.Content.ReadAsAsync<UserModel>();

            _homeViewModel.IsLoading = false;
        }
    }
}