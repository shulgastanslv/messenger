using Client.ViewModels;

namespace Client.Commands;

public class HidePanelCommand : ViewModelCommand
{
    private readonly HomeViewModel _homeViewModel;

    public HidePanelCommand(HomeViewModel homeViewModel)
    {
        _homeViewModel = homeViewModel;
    }
    public override void Execute(object? parameter)
    {
        _homeViewModel.IsPanelVisible = false;
    }
}