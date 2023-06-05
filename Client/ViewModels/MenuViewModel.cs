using System.Net.Http;
using System.Windows.Input;
using Client.Commands;
using Client.Models;
using Client.Services;
using Client.Stores;

namespace Client.ViewModels;

public class MenuViewModel : ViewModelBase
{
    private readonly UserModel _userStore;
    public string UserName => _userStore.UserName!;

    private readonly NavigationStore _navigationStore = new();

    public ICommand CloseMenuCommand { get; }
    public MenuViewModel(NavigationStore menuStore, UserModel userStore)
    {
        _userStore = userStore;

        CloseMenuCommand = new NavigateCommand<MenuViewModel>(
            new NavigationService<MenuViewModel>(
            menuStore, () => null));
    }
}