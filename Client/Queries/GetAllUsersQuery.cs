using System.Collections.ObjectModel;
using Client.Stores;
using Client.ViewModels;
using System.Net.Http;
using Client.Models;
using System.Net.Http.Headers;
using Client.Commands;

namespace Client.Queries;

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

        Execute(null);
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