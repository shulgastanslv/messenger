using System;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Client.Interfaces;
using Client.Models;
using Newtonsoft.Json;

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
    public ICommand SignInCommand { get; set; }

    public SignInViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        NavigateToSignUpCommand = new ViewModelCommand(ExecuteNavigationToSignUpCommand);
        SignInCommand = new ViewModelCommand(ExecuteSignInCommand, CanExecuteSignInCommand);
    }

    private async void ExecuteSignInCommand(object obj)
    {
      
    }
    private bool CanExecuteSignInCommand(object obj)
    {
        if (string.IsNullOrWhiteSpace(Email) ||
            string.IsNullOrWhiteSpace(Password) || Password.Length < 16 || !Email.Contains("@") || !Email.Contains(".") ||
            Email.Length == 1)
            return false;

        return true;
    }
    private void ExecuteNavigationToSignUpCommand(object obj)
    {
        NavigationService.NavigateTo<SignUpViewModel>();
    }
}