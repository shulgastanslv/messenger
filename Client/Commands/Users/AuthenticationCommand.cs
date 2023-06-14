using System;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using Client.Models;
using Client.Properties;
using Client.Services;
using Client.Stores;
using Client.ViewModels;
using Newtonsoft.Json;

namespace Client.Commands.Users;

public record ReceivedData(string Token, Guid Id);

public sealed class AuthenticationCommand : CommandBase
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
        PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(AuthenticationViewModel.UserName) ||
            e.PropertyName == nameof(AuthenticationViewModel.Password))
            OnCanExecutedChanged();
    }

    public override bool CanExecute(object? parameter)
    {
        return !string.IsNullOrEmpty(_authenticationViewModel.UserName) &&
               !string.IsNullOrEmpty(_authenticationViewModel.Password);
    }

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
            var receivedData = await response.Content.ReadAsAsync<ReceivedData>();

            _userStore.User.Id = receivedData.Id;
            _userStore.Token = receivedData.Token.Trim('"');

            _navigationService.Navigate();

        }

        _authenticationViewModel.IsLoading = false;
    }
}