using Client.Models;
using Client.ViewModels;
using System.Collections.ObjectModel;
using System.Net.Http;
using Client.Commands;

namespace Client.Queries;

public class GetUsersByUsernameQuery : ViewModelCommand
{
    private readonly HomeViewModel _homeViewModel;

    private readonly HttpClient _httpClient;

    public GetUsersByUsernameQuery(HomeViewModel homeViewModel,
        HttpClient httpClient)
    {
        _homeViewModel = homeViewModel;
        _httpClient = httpClient;
    }

    public override async void Execute(object? parameter)
    {
        var response = await _httpClient.GetAsync("/users/getUsersByUsername"
                                                  + $"?Username={_homeViewModel.SearchText}");
        if (!response.IsSuccessStatusCode) return;

        _homeViewModel.Contacts = await response.Content
            .ReadAsAsync<ObservableCollection<ContactModel>>();
    }
}