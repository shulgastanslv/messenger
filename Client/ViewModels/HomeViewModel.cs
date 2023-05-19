using System;
using System.Collections.ObjectModel;
using Client.Interfaces;
using Client.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Client.ViewModels;

public class HomeViewModel : ViewModel
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

    public HomeViewModel(INavigationService navigationService)
    {
        NavigationService = navigationService;
    }
}