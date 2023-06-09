using System;
using System.ComponentModel;
using Client.Services;
using Client.Stores;
using Client.ViewModels;
using System.Net.Http;
using System.Text;
using Client.Models;
using Newtonsoft.Json;

namespace Client.Commands.Users;

public class RegistrationCommand : ViewModelCommand
{
    private readonly RegistrationViewModel _registrationViewModel;

    private readonly HttpClient _httpClient;

    private readonly NavigationService<HomeViewModel> _navigationService;

    private readonly UserStore _userStore;

    public RegistrationCommand(RegistrationViewModel registrationViewModel, UserStore userStore, 
        HttpClient httpClient, NavigationService<HomeViewModel> navigationService)
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
        {
            OnCanExecutedChanged();
        }
    }

    public override bool CanExecute(object? parameter) =>
        !string.IsNullOrEmpty(_registrationViewModel.UserName) &&
        !string.IsNullOrEmpty(_registrationViewModel.Password) && 
        _registrationViewModel.IsAgree;

    public override async void Execute(object? parameter)
    {
        _registrationViewModel.IsLoading = true;

        _userStore.User = new UserModel(Guid.NewGuid(), _registrationViewModel.UserName,
            _registrationViewModel.Password);

        var content = new StringContent(JsonConvert.SerializeObject(_userStore.User),
            Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("/registration/reg", content);

        response.EnsureSuccessStatusCode();

        if (response.IsSuccessStatusCode)
        {
            _userStore.Token = await response.Content.ReadAsStringAsync();

            _userStore.Token = _userStore.Token.Trim('"');
            _userStore.Token = _userStore.Token.Trim('\\');

            _navigationService.Navigate();
        }

        Properties.Settings.Default.UserName = _userStore.User.UserName;
        Properties.Settings.Default.Password = _userStore.User.Password;
        Properties.Settings.Default.Token = _userStore.Token;
        Properties.Settings.Default.Save();

        _registrationViewModel.IsLoading = false;

    }
}