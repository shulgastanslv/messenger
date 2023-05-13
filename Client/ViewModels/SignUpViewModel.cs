using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Client.Models;
using Client.Interfaces;

namespace Client.ViewModels;

public class SignUpViewModel : ViewModel
{
    private string _userName;
    private string _email;
    private string _password;
    private INavigationService _navigationService;

    public string UserName
    {
        get => _userName;
        set
        {
            _userName = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged(nameof(UserName));
        }
    }
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

    public ICommand SignUpCommand { get; set; }
    
    public SignUpViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        SignUpCommand = new ViewModelCommand(ExecuteSignUpCommand, CanExecuteSignUpCommand);
    }

    private bool CanExecuteSignUpCommand(object obj)
    {
        if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Email)  || string.IsNullOrWhiteSpace(Password) ||
            UserName.Length == 1 || Password.Length < 16 || !Email.Contains("@") || !Email.Contains(".") || Email.Length == 1)
            return false;

        return true;
    }

    private async void ExecuteSignUpCommand(object obj)
    {
        using (var httpClient = new HttpClient())
        {
            var content = new StringContent(JsonConvert.SerializeObject(new UserModel()
            {
                Id = Guid.NewGuid(),
                UserName = UserName,
                Email = Email,
                Password = Password,
                CreationTime = DateTime.Now
            }), Encoding.UTF8, "application/json");


            var response = await httpClient.PostAsync("https://localhost:7289/api/User/CreateUser", content);

            await response.Content.ReadAsStringAsync();
        }

    }
}