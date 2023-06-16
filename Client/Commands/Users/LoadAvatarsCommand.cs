using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading;
using Client.Models;
using Client.Services;
using Client.ViewModels;

namespace Client.Commands.Users;

public class LoadAvatarsCommand : CommandBase
{
    private readonly HomeViewModel _homeViewModel;
    private readonly HttpClient _httpClient;


    public LoadAvatarsCommand(HomeViewModel homeViewModel, HttpClient httpClient)
    {
        _homeViewModel = homeViewModel;
        _httpClient = httpClient;
    }

    public override async void Execute(object? parameter)
    {
        //foreach (var item in _homeViewModel.Contacts)
        //    await AvatarService.LoadAvatarAsync(item, CancellationToken.None);
    }
}