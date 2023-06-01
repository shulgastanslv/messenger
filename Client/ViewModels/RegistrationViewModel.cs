using System.Net.Http;
using System.Windows.Input;
using Client.Commands;
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
        get => _userStore.User.UserName!;
        set
        {
            _userStore.User.UserName = value;
            OnPropertyChanged(nameof(UserName));
        }
    }

    public string Email
    {
        get => _userStore.User.Email!;
        set
        {
            _userStore.User.Email = value;
            OnPropertyChanged(nameof(Email));
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

    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            _isLoading = value;
            OnPropertyChanged(nameof(IsLoading));
        }
    }
    public ICommand NavigateCommand { get; }
    public ICommand RegistrationCommand { get; }

    public RegistrationViewModel(HttpClient httpClient, UserStore userStore, NavigationStore navigationStore)
    {
        NavigateCommand = new NavigateCommand<AuthenticationViewModel>(
            new NavigationService<AuthenticationViewModel>(navigationStore,
                () => new AuthenticationViewModel(userStore, httpClient, navigationStore)));

        var navigateService = new NavigationService<HomeViewModel>(
            navigationStore,
            () => new HomeViewModel(userStore, httpClient));

        _userStore = userStore;

        RegistrationCommand = new RegistrationCommand(this, httpClient, userStore, navigateService);
    }
}