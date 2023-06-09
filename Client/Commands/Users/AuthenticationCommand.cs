using System;
using Client.Models;
using Client.Services;
using Client.Stores;
using Client.ViewModels;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Client.Commands.Users;

public sealed class AuthenticationCommand : ViewModelCommand
{
    private readonly AuthenticationViewModel _authenticationViewModel;

    private readonly HttpClient _httpClient;

    private readonly NavigationService<HomeViewModel> _navigationService;

    private readonly UserStore _userStore;

    public AuthenticationCommand(AuthenticationViewModel authenticationViewModel,
        UserStore userStore, HttpClient httpClient, NavigationService<HomeViewModel> navigationService)
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
        if (e.PropertyName == nameof(AuthenticationViewModel.UserName) ||
            e.PropertyName == nameof(AuthenticationViewModel.Password))
        {
            OnCanExecutedChanged();
        }
    }

    public override bool CanExecute(object? parameter) =>
        !string.IsNullOrEmpty(_authenticationViewModel.UserName) &&
        !string.IsNullOrEmpty(_authenticationViewModel.Password);

    public override async void Execute(object? parameter)
    {
        _authenticationViewModel.IsLoading = true;

        _userStore.User = new UserModel(
            Guid.Empty,
            _authenticationViewModel.UserName,
            _authenticationViewModel.Password);

        var content = new StringContent(JsonConvert.SerializeObject(_userStore.User),
            Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("/authentication/auth", content);

        response.EnsureSuccessStatusCode();

        if (response.IsSuccessStatusCode)
        {
            _userStore.Token = await response.Content.ReadAsStringAsync();

            _userStore.Token = _userStore.Token.Trim('"');

            _navigationService.Navigate();
        }

        Properties.Settings.Default.UserName = _userStore.User.UserName;
        Properties.Settings.Default.Password = _userStore.User.Password;
        Properties.Settings.Default.Token = _userStore.Token;
        Properties.Settings.Default.Save();

        _authenticationViewModel.IsLoading = false;
    }
}
