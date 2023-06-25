using Client.Interfaces;
using Client.Services;
using Client.Stores;

namespace Client.Commands.Navigation;

public class LogoutCommand : CommandBase
{
    private readonly INavigationService _navigationService;
    private readonly UserStore _userStore;

    public LogoutCommand(UserStore userStore, INavigationService navigationService)
    {
        _userStore = userStore;
        _navigationService = navigationService;
    }

    public override void Execute(object? parameter)
    {
        UserStoreSettingsService.DeleteUserStore(_userStore);
        _navigationService.Navigate();
    }
}