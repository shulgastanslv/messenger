using System.Net.Http;
using System.Windows.Input;
using Client.Commands.Navigation;
using Client.Commands.Users;
using Client.Interfaces;
using Client.Stores;

namespace Client.ViewModels;

public sealed class AuthenticationViewModel : ViewModelBase
{
    private readonly UserStore _userStore;

    private bool _isLoading;

    public AuthenticationViewModel(UserStore userStore, HttpClient httpClient,
        INavigationService registrationNavigationService,
        INavigationService homeNavigationService)
    {
        _userStore = userStore;

        NavigateToRegistrationCommand = new NavigateCommand(registrationNavigationService);

        AuthenticationCommand = new AuthenticationCommand(this, userStore, httpClient, homeNavigationService);
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

    public string UserName
    {
        get => _userStore.User.Username!;
        set
        {
            _userStore.User.Username = value;
            OnPropertyChanged(nameof(UserName));
        }
    }

    public string Password
    {
        get => _userStore.User.Password!;
        set
        {
            _userStore.User.Password = value;
            OnPropertyChanged(nameof(Password));
        }
    }

    public ICommand AuthenticationCommand { get; }
    public ICommand NavigateToRegistrationCommand { get; }
}