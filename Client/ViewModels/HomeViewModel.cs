using System;
using Client.Interfaces;

namespace Client.ViewModels;

public class HomeViewModel : ViewModel
{
    private string _message;

    public string Message
    {
        get => _message;
        set
        {
            _message = value;
            OnPropertyChanged();
        }
    }

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



    public HomeViewModel(INavigationService navigationService)
    {
        NavigationService = navigationService;
    }
}