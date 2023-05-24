using System;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Client.Interfaces;
using Client.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Client.ViewModels;

public class SignInViewModel : ViewModel
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

    private string _email;

    private string _password;
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

    public ICommand NavigateToSignUpCommand { get; set; }
    public ICommand SignInCommand { get; set; }

    public SignInViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;

        SignInCommand = new ViewModelCommand(ExecuteSignInCommand, CanExecuteSignInCommand);

        NavigateToSignUpCommand = new ViewModelCommand(i =>
            NavigationService.NavigateTo<SignUpViewModel>());
    }

    private bool CanExecuteSignInCommand(object obj)
    {
        //if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password) ||
        //    Password.Length < 16 || !Email.Contains("@") || !Email.Contains(".") || Email.Length == 1)
        //    return false;

        return true;
    }

    private async void ExecuteSignInCommand(object obj)
    {
        using var httpClient = new HttpClient();

        var userModel = new UserModel
        {
            Email = Email,
            Password = Password,
        };

        var content = new StringContent(JsonConvert.SerializeObject(userModel), 
            Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync("https://localhost:7289/authenticate/auth", content);

        await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            NavigationService.NavigateTo<HomeViewModel>();
        }
    }
}