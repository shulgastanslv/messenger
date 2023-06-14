using Client.Services;
using Client.Stores;
using Client.ViewModels;

namespace Client.Commands;

public class LogoutCommand : CommandBase
{
    private readonly UserStore _userStore;

    private readonly NavigationService<AuthenticationViewModel> _navigationService;
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