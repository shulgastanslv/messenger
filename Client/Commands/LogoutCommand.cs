using Client.Services;
using Client.Stores;
using Client.ViewModels;

namespace Client.Commands;

public class LogoutCommand : CommandBase
{
    private readonly NavigationService<AuthenticationViewModel> _navigationService;
    private readonly UserStore _userStore;

    public LogoutCommand(UserStore userStore, NavigationService<AuthenticationViewModel> navigationService)
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