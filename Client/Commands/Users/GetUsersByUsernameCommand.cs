using System.Collections.ObjectModel;
using System.Net.Http;
using Client.Models;
using Client.ViewModels;

namespace Client.Commands.Users;

public class GetUsersByUsernameCommand : CommandBase
{
    private readonly HomeViewModel _homeViewModel;

    private readonly HttpClient _httpClient;

    public GetUsersByUsernameCommand(HomeViewModel homeViewModel,
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