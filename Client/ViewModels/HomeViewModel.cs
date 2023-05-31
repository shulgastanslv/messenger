using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Client.Interfaces;
using Client.Models;

namespace Client.ViewModels;

public class HomeViewModel : ViewModel
{
    private UserModel _selfModel;
    public UserModel SelfModel
    {
        get => _selfModel;
        set
        {
            _selfModel = value;
            OnPropertyChanged(nameof(SelfModel));
        }
    }

    private List<UserChatViewModel> _users;
    public List<UserChatViewModel> Users
    {
        get => _users;
        set
        {
            _users = value;
            OnPropertyChanged(nameof(Users));
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