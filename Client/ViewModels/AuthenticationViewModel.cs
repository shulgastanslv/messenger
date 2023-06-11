using System.Net.Http;
using System.Windows.Input;
using Client.Commands;
using Client.Commands.Users;
using Client.Services;
using Client.Stores;

namespace Client.ViewModels;

public sealed class AuthenticationViewModel : ViewModelBase
{
    private readonly UserStore _userStore;

    private bool _isLoading;

    public AuthenticationViewModel(UserStore userStore, HttpClient httpClient, NavigationStore navigationStore)
    {
        _userStore = userStore;

        NavigateToRegistrationCommand = new NavigateCommand<RegistrationViewModel>(
            new NavigationService<RegistrationViewModel>(navigationStore,
                () => new RegistrationViewModel(userStore, httpClient, navigationStore)));

        var navigationService = new NavigationService<HomeViewModel>(
            navigationStore,
            () => new HomeViewModel(userStore, httpClient));

        AuthenticationCommand = new AuthenticationCommand(this, userStore, httpClient, navigationService);
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
        get => _userStore.User.UserName!;
        set
        {
            _userStore.User.UserName = value;
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