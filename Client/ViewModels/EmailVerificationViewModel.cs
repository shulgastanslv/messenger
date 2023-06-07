using Client.Stores;
using System.Net.Http;
using System.Windows.Input;
using Client.Commands;
using Client.Services;
using System;
using Client.Commands.EmailVerification;

namespace Client.ViewModels;

public class EmailVerificationViewModel : ViewModelBase
{
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

    private string? _code;
    public string? Code
    {
        get => _code;
        set
        {
            _code = value;
            OnPropertyChanged(nameof(Code));
        }
    }
    public ICommand EmailVerificationCommand { get; }
    public ICommand SendVerificationCodeCommand { get; }
    public ICommand NavigateBackCommand { get; }
    public EmailVerificationViewModel(UserStore userStore, HttpClient httpClient, NavigationStore navigationStore)
    {
        NavigateBackCommand = new NavigateCommand<AuthenticationViewModel>(
            new NavigationService<AuthenticationViewModel>(
                navigationStore, () => new AuthenticationViewModel(userStore, httpClient, navigationStore)));

        var navigationService = new NavigationService<HomeViewModel>(
            navigationStore,
            () => new HomeViewModel(userStore, httpClient));

        var code = new Random().Next(100000, 999999);

        SendVerificationCodeCommand = new SendVerificationCodeCommand(userStore, httpClient, 
            this, code);

        SendVerificationCodeCommand.Execute(null);

        EmailVerificationCommand = new EmailVerificationCommand(userStore, httpClient, 
            navigationService, this, code);
    }
}