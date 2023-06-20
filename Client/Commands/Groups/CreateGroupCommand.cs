using System.Net.Http;
using Client.Interfaces;
using Client.Services;
using Client.ViewModels;

namespace Client.Commands.Groups;
public class CreateGroupCommand : CommandBase
{
    private readonly HttpClient _httpClient;
    private readonly CreateGroupViewModel _createGroupViewModel;
    private readonly INavigationService _navigationService;


    public CreateGroupCommand(HttpClient httpClient, CreateGroupViewModel createGroupViewModel
        , INavigationService navigationService)
    {
        _httpClient = httpClient;
        _createGroupViewModel = createGroupViewModel;
        _navigationService = navigationService;
    }

    public override async void Execute(object? parameter)
    {
        var name = new GroupName(_createGroupViewModel.GroupName!);

        if (_createGroupViewModel.GroupName != null)
            await GroupService.CreateGroupAsync(name, _httpClient);

        _navigationService.Navigate();
    }
}