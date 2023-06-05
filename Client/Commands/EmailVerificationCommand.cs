using System;
using System.ComponentModel;
using Client.Services;
using Client.Stores;
using Client.ViewModels;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Client.Models;

namespace Client.Commands;

public class EmailVerificationCommand : ViewModelCommand
{
    private readonly HttpClient _httpClient;

    private readonly NavigationService<HomeViewModel> _navigationService;

    private readonly UserStore _userStore;

    private readonly EmailVerificationViewModel _emailVerificationViewModel;

    private readonly int _code;

    public EmailVerificationCommand(UserStore userStore, HttpClient httpClient,
        NavigationService<HomeViewModel> navigationService,
        EmailVerificationViewModel emailVerificationViewModel, int code)
    {
        _userStore = userStore;
        _httpClient = httpClient;
        _navigationService = navigationService;
        _emailVerificationViewModel = emailVerificationViewModel;
        _code = code;
        _emailVerificationViewModel.PropertyChanged += OnPropertyChanged;
    }

    private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(EmailVerificationViewModel.Code))
            OnCanExecutedChanged();
    }

    public override bool CanExecute(object? parameter) =>
        !string.IsNullOrEmpty(_emailVerificationViewModel.Code) &&
        _emailVerificationViewModel.Code == _code.ToString();

    public override void Execute(object? parameter)
    {
        _navigationService.Navigate();
    }

}