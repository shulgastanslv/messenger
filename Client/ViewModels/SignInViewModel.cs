using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Client.Interfaces;
using Client.Models;
using Client.Services;
using Domain.Entities;
using Microsoft.Identity.Client;
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
        SignInCommand = new ViewModelCommand(ExecuteSignInCommand, CanExecuteSignInCommand);
        NavigateToSignUpCommand = new ViewModelCommand(i =>
            NavigationService.NavigateTo<SignUpViewModel>());
    }

    private bool CanExecuteSignInCommand(object obj)
    {
        if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password) ||
            Password.Length < 16 || !Email.Contains("@") || !Email.Contains(".") || Email.Length == 1)
            return false;

        return true;
    }

    private async void ExecuteSignInCommand(object obj)
    {
        using (var httpClient = new HttpClient())
        {
            var content = new StringContent(JsonConvert.SerializeObject(new UserModel()
            {
                Email = Email,
                Password = Password,
            }), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("https://localhost:7289/api/Authenticate", content);

            await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                NavigationService.NavigateTo<HomeViewModel>();
            }
        }


    }
}