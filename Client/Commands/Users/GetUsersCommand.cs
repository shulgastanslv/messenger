using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using Client.Models;
using Client.Services;
using Client.Stores;
using Client.ViewModels;

namespace Client.Commands.Users;

public class GetUsersQuery : CommandBase
{
    private readonly HomeViewModel _homeViewModel;

    private readonly HttpClient _httpClient;

    private readonly UserStore _userStore;

    public GetUsersQuery(HomeViewModel homeViewModel, HttpClient httpClient, UserStore userStore)
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

        var response = await _httpClient.GetAsync("/users/get");

        if (!response.IsSuccessStatusCode)
            return;

        var contacts = await response.Content
            .ReadAsAsync<ObservableCollection<ContactModel>>();

        _homeViewModel.Contacts = contacts;

        if (contacts == null)
            return;

        foreach (var contact in contacts)
            SaveEntityModelService.SaveEntity(contact);

        _homeViewModel.IsLoading = false;
    }
}