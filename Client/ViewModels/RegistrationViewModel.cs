using System.Net.Http;
using System.Windows.Input;
using Client.Commands.Navigation;
using Client.Commands.Users;
using Client.Interfaces;
using Client.Stores;

namespace Client.ViewModels;

public class RegistrationViewModel : ViewModelBase
{
    private readonly UserStore _userStore;

    private bool _isAgree;

    private bool _isLoading;

    public RegistrationViewModel(UserStore userStore, HttpClient httpClient,
        INavigationService authenticationNavigationService,
        INavigationService homeNavigationService)
    {
        _userStore = userStore;

        NavigateToAuthenticationCommand = new NavigateCommand(authenticationNavigationService);

        RegistrationCommand = new RegistrationCommand(this, userStore, httpClient,
            homeNavigationService);
    }

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
        get => _userStore.User.Username;
        set
        {
            _userStore.User.Username = value;
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
}