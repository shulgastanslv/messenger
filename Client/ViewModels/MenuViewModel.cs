using System.Net.Http;
using System.Windows.Input;
using Client.Commands;
using Client.Services;
using Client.Stores;

namespace Client.ViewModels;

public class MenuViewModel : ViewModelBase
{
    private readonly UserStore _userStore;
    public string UserName => _userStore.User.UserName!;

    private readonly NavigationStore _navigationStore = new();

    public ICommand CloseMenuCommand { get; }
    public MenuViewModel(NavigationStore menuStore, UserStore userStore)
    {
        _userStore = userStore;

        CloseMenuCommand = new NavigateCommand<MenuViewModel>(
            new NavigationService<MenuViewModel>(
            menuStore, () => null));
    }
}