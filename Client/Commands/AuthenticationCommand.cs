using Client.Models;
using Client.Services;
using Client.Stores;
using Client.ViewModels;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Client.Commands;

public class AuthenticationCommand : ViewModelCommand
{
    private readonly AuthenticationViewModel _authenticationViewModel;

    private readonly HttpClient _httpClient;

    private readonly NavigationService<HomeViewModel> _navigationService;

    private readonly UserStore _userStore;

    public AuthenticationCommand(AuthenticationViewModel authenticationViewModel,
        HttpClient httpClient, UserStore userStore, NavigationService<HomeViewModel> navigationService)
    {
        _authenticationViewModel = authenticationViewModel;
        _httpClient = httpClient;
        _userStore = userStore;

        _navigationService = navigationService;

        authenticationViewModel.PropertyChanged += OnPropertyChanged;
    }

    private void OnPropertyChanged(object? sender,
        System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(AuthenticationViewModel.Email) ||
            e.PropertyName == nameof(AuthenticationViewModel.Password))
        {
            OnCanExecutedChanged();
        }
    }

    public override bool CanExecute(object? parameter) =>
        !string.IsNullOrEmpty(_authenticationViewModel.Email) &&
        !string.IsNullOrEmpty(_authenticationViewModel.Password);

    public override async void Execute(object? parameter)
    {
        _authenticationViewModel.IsLoading = true;

        _userStore.User = new UserModel
        {
            Email = _authenticationViewModel.Email,
            Password = _authenticationViewModel.Password,
        };

        var content = new StringContent(JsonConvert.SerializeObject(_userStore.User),
            Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("/authentication/auth", content);

        response.EnsureSuccessStatusCode();

        if (response.IsSuccessStatusCode)
        {
            _userStore.Token = await response.Content.ReadAsStringAsync();


            //ЧТО С СОХРАНЕНИЕМ???
            _userStore.Token = _userStore.Token.Trim('"');
            _userStore.Token = _userStore.Token.Trim('\\');

            _navigationService.Navigate();
        }

        _authenticationViewModel.IsLoading = false;
    }
}
