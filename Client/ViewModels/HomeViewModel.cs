using System.Collections.Generic;
using Client.Stores;
using System.Net.Http;
using System.Windows.Input;
using Client.Models;
using Client.Queries;
using System.Linq;
using Client.Commands;
using Client.Services;

namespace Client.ViewModels;

public class HomeViewModel : ViewModelBase
{
    private UserStore _userStore;
    public UserStore UserStore
    {
        get => _userStore;
        set
        {
            _userStore = value;
            OnPropertyChanged(nameof(UserStore));
        }
    }

    private List<UserModel> _users = new();

    private readonly NavigationStore _chatStore = new();

    private readonly NavigationStore _menuStore = new();
    public List<UserModel> Users
    {
        get => _users;
        set
        {
            _users = value;
            OnPropertyChanged(nameof(Users));
        }
    }

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

    private UserModel? _selectedUser;
    public UserModel? SelectedUser
    {
        get => _selectedUser;
        set
        {
            _selectedUser = value;
            OnPropertyChanged(nameof(SelectedUser));
            _chatStore.CurrentViewModel = new ChatViewModel(_selectedUser);
            OnPropertyChanged(nameof(CurrentChatViewModel));
        }
    }
    public ICommand OpenMenuCommand { get; }
    public ICommand GetAllUsersCommand { get; }
    public ICommand GetUserByEmailCommand { get; }

    public HomeViewModel(UserStore userStore, HttpClient httpClient)
    {
        _userStore = userStore;

        GetAllUsersCommand = new GetAllUsersQuery(this, _userStore, httpClient);

        GetUserByEmailCommand = new GetUserByEmailQuery(this, _userStore, httpClient);

        OpenMenuCommand = new NavigateCommand<MenuViewModel>(
                new NavigationService<MenuViewModel>(_menuStore,
                    () => new MenuViewModel(_menuStore, _userStore.User)));

        _menuStore.CurrentViewModelChanged += () =>
        {
            OnPropertyChanged(nameof(MenuViewModel));
        };
    }

    public ViewModelBase CurrentChatViewModel => _chatStore.CurrentViewModel;
    public ViewModelBase MenuViewModel => _menuStore.CurrentViewModel;
}