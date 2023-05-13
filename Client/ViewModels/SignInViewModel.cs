using System;
using System.Windows.Input;
using Client.Interfaces;

namespace Client.ViewModels;

public class SignInViewModel : ViewModel
{
    private string _email;
    private string _password;
    private INavigationService _navigationService;

    public string Email
    {
        get => _email;
        set
        {
            _email = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged(nameof(Email));
        }
    }
    public string Password
    {
        get => _password;
        set
        {
            _password = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged(nameof(Password));
        }
    }
    public INavigationService NavigationService
    {
        get => _navigationService;
        set
        {
            _navigationService = value;
            OnPropertyChanged();
        }
    }
    public ICommand NavigateToSignUpCommand { get; set; }

    public SignInViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        NavigateToSignUpCommand = new ViewModelCommand(ExecuteNavigationToSignUpCommand, CanExecuteNavigationToSignUpCommand);
    }

    private bool CanExecuteNavigationToSignUpCommand(object obj)
    {
        return true;
    }
    private void ExecuteNavigationToSignUpCommand(object obj)
    {
        NavigationService.NavigateTo<SignUpViewModel>();
    }
}