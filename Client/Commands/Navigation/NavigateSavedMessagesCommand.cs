using System.Linq;
using Client.Stores;
using Client.ViewModels;

namespace Client.Commands.Navigation;

public class NavigateSavedMessagesCommand : CommandBase
{
    private readonly HomeViewModel _homeViewModel;
    private readonly UserStore _userStore;

    public NavigateSavedMessagesCommand(HomeViewModel homeViewModel, UserStore userStore)
    {
        _homeViewModel = homeViewModel;
        _userStore = userStore;
    }

    public override void Execute(object? parameter)
    {
        var user = _homeViewModel.Contacts.FirstOrDefault(i => i.Username == _userStore.User.Username);

        _homeViewModel.SelectedContact = user!;

        _homeViewModel.IsSelectedUser = true;
    }
}