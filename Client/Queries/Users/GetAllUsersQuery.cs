using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using Client.Commands;
using Client.Models;
using Client.Stores;
using Client.ViewModels;

namespace Client.Queries.Users;

public sealed class GetAllUsersQuery : ViewModelCommand
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

    public override async void Execute(object? parameter)
    {
        _homeViewModel.IsLoading = true;

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
            _userStore.Token);

        var response = await _httpClient.GetAsync("https://localhost:7289/users/getAllUsers");

        if (response.IsSuccessStatusCode)
        {
            _homeViewModel.Contacts = await response.Content.ReadAsAsync<ObservableCollection<ContactModel>>();

            _homeViewModel.IsLoading = false;
        }
    }
}