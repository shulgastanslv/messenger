using System.Net.Http;
using Client.Services;
using Client.Stores;
using Client.ViewModels;

namespace Client.Commands;

public class ShowPanelCommand : ViewModelCommand
{
    private readonly HomeViewModel _homeViewModel;

    public ShowPanelCommand(HomeViewModel homeViewModel)
    {
        _homeViewModel = homeViewModel;
    }
    public override void Execute(object? parameter)
    {
        _homeViewModel.IsPanelVisible = true;
    }
}