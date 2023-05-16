using System;
using System.Net.Http;
using System.Windows.Input;
using Client.Interfaces;

namespace Client.ViewModels;

public class MainViewModel : ViewModel
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

    public MainViewModel(INavigationService navigationService)
    {
        NavigationService = navigationService;

        NavigationService.NavigateTo<SignInViewModel>();
    }
}