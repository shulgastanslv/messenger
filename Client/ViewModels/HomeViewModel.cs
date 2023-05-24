using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Client.Interfaces;
using Client.Models;

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