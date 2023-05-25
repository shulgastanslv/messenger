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
    private List<UserChatViewModel> _users;

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

    public List<UserChatViewModel> Users
    {
        get => _users;
        set
        {
            _users = value;
            OnPropertyChanged(nameof(Users));
        }
    }

    public HomeViewModel(INavigationService navigationService)
    {
        NavigationService = navigationService;

        Users = new List<UserChatViewModel>
        {
            new(navigationService)
            {
                User = new UserModel()
                {
                    UserName = "akiroqw",
                    LastMessage = "test"
                }
            }
        };
    }
}