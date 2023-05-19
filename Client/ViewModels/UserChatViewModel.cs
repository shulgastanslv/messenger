using Client.Interfaces;

namespace Client.ViewModels;

public class UserChatViewModel : ViewModel
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

    public UserChatViewModel(INavigationService navigationService)
    {
        NavigationService = navigationService;
    }
}