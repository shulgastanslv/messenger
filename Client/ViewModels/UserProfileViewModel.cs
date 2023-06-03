using System;
using Client.Stores;
using System.Net.Http;
using System.Windows;
using System.Windows.Input;
using Client.Commands;
using Client.Services;

namespace Client.ViewModels;

public class UserProfileViewModel : ViewModelBase
{
    private UserStore _userStore;
    public string UserName => _userStore.User.UserName;
    public string? Email => _userStore.User.Email;
    public string? Password => _userStore.User.Password;
    public string ? Id => _userStore.User.Id.ToString();
    public string? CreationTime => _userStore.User.CreationTime.ToString();

    private bool _isLoading;
    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            _isLoading = value;
            OnPropertyChanged(nameof(IsLoading));
        }
    }

    public UserProfileViewModel(HttpClient httpClient, UserStore userStore, 
        NavigationStore navigationStore)
    {
        _userStore = userStore;
    }
}