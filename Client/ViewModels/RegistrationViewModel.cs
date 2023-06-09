using System.Net.Http;
using System.Windows.Input;
using Client.Commands;
using Client.Commands.Users;
using Client.Services;
using Client.Stores;

namespace Client.ViewModels;

public class RegistrationViewModel : ViewModelBase
{
    private readonly UserStore _userStore;

    private bool _isLoading;

    private bool _isAgree;
    public bool IsAgree
    {
        get => _isAgree;
        set
        {
            _isAgree = value;
            OnPropertyChanged(nameof(IsAgree));
        }
    }
    public string UserName
    {
        get => _userStore.User.UserName;
        set
        {
            _userStore.User.UserName = value;
            OnPropertyChanged(nameof(UserName));
        }
    }
    public string Password
    {
        get => _userStore.User.Password;
        set
        {
            _userStore.User.Password = value;
            OnPropertyChanged(nameof(Password));
        }
    }
    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            _isLoading = value;
            OnPropertyChanged(nameof(IsLoading));
        }
    }
    public ICommand NavigateToAuthenticationCommand { get; }
    public ICommand RegistrationCommand { get; }

    public RegistrationViewModel(UserStore userStore, HttpClient httpClient, NavigationStore navigationStore)
    {
        _userStore = userStore;

        NavigateToAuthenticationCommand = new NavigateCommand<AuthenticationViewModel>(
            new NavigationService<AuthenticationViewModel>(navigationStore,
                () => new AuthenticationViewModel(userStore, httpClient, navigationStore)));

        var navigateService = new NavigationService<HomeViewModel>(
            navigationStore,
            () => new HomeViewModel(userStore, httpClient));

        RegistrationCommand = new RegistrationCommand(this, userStore, httpClient, 
            navigateService);
    }
}