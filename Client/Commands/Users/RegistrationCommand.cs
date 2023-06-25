using System;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using Client.Interfaces;
using Client.Models;
using Client.Stores;
using Client.ViewModels;
using Newtonsoft.Json;

namespace Client.Commands.Users;

public class RegistrationCommand : CommandBase
{
    private readonly HttpClient _httpClient;

    private readonly INavigationService _navigationService;

    private readonly RegistrationViewModel _registrationViewModel;

    private readonly UserStore _userStore;

    public RegistrationCommand(RegistrationViewModel registrationViewModel, UserStore userStore,
        HttpClient httpClient, INavigationService navigationService)
    {
        _userStore = userStore;
        _httpClient = httpClient;
        _navigationService = navigationService;
        _registrationViewModel = registrationViewModel;

        _registrationViewModel.PropertyChanged += OnPropertyChanged;
    }

    private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(RegistrationViewModel.UserName) ||
            e.PropertyName == nameof(RegistrationViewModel.Password) ||
            e.PropertyName == nameof(RegistrationViewModel.IsAgree))
            OnCanExecutedChanged();
    }

    public override bool CanExecute(object? parameter)
    {
        return !string.IsNullOrEmpty(_registrationViewModel.UserName) &&
               !string.IsNullOrEmpty(_registrationViewModel.Password) &&
               _registrationViewModel.IsAgree;
    }

    public override async void Execute(object? parameter)
    {
        _registrationViewModel.IsLoading = true;

        _userStore.User = new UserModel(Guid.Empty, _registrationViewModel.UserName,
            _registrationViewModel.Password);

        var content = new StringContent(JsonConvert.SerializeObject(_userStore.User),
            Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("/registration/reg", content);

        response.EnsureSuccessStatusCode();

        if (response.IsSuccessStatusCode)
        {
            var receivedData = await response.Content.ReadAsAsync<ReceivedData>();

            _userStore.Token = receivedData.Token.Trim('"');

            _userStore.User.Id = receivedData.Id;

            _navigationService.Navigate();
        }

        _registrationViewModel.IsLoading = false;
    }
}