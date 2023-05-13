using Client.Interfaces;

namespace Client.ViewModels;

public class AccountRecoveryViewModel : ViewModel
{
    private INavigationService _navigationService;

    public INavigationService NavigationService
    {
        get => _navigationService;
        set
        {
            _navigationService = value;
            OnPropertyChanged();
        }
    }
    public AccountRecoveryViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }
}