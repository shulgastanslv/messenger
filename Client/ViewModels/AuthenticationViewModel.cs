using System.Net.Http;
using System.Windows;
using System.Windows.Input;
using Client.Commands;
using Client.Services;
using Client.Stores;
using Domain.Entities.Users;

namespace Client.ViewModels;

public sealed class AuthenticationViewModel : ViewModelBase
{
    private readonly UserStore _userStore;

    private bool _isLoading;
    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            _isLoading = value;
            OnPropertyChanged(nameof(IsLoading));
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
    public ICommand AuthenticationCommand { get; }
    public ICommand NavigateToRegistrationCommand { get; }
    public ICommand NavigateBackCommand { get; }
    public AuthenticationViewModel(UserStore userStore, HttpClient httpClient, NavigationStore navigationStore)
    {
        _userStore = userStore;

        NavigateToRegistrationCommand = new NavigateCommand<RegistrationViewModel>(
            new NavigationService<RegistrationViewModel>(navigationStore,
                () => new RegistrationViewModel(userStore, httpClient, navigationStore)));

        NavigateBackCommand = new NavigateCommand<WelcomeViewModel>(
            new NavigationService<WelcomeViewModel>(navigationStore,
                () => new WelcomeViewModel(userStore, httpClient, navigationStore)));

        var navigationService = new NavigationService<EmailVerificationViewModel>(
            navigationStore,
            () => new EmailVerificationViewModel(userStore, httpClient, navigationStore));

        AuthenticationCommand = new AuthenticationCommand(this, httpClient, userStore, navigationService);
    }
}