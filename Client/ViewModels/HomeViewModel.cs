using System.Collections.Generic;
using Client.Stores;
using System.Net.Http;
using System.Windows.Input;
using Client.Models;
using Client.Queries;
using System.Linq;
using Client.Commands;
using Client.Services;
using System.Collections.ObjectModel;

namespace Client.ViewModels;

public class HomeViewModel : ViewModelBase
{
    private readonly HttpClient _httpClient;

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

    private ObservableCollection<ContactModel> _contacts = new();
    public ObservableCollection<ContactModel> Contacts
    {
        get => _contacts;
        set
        {
            _contacts = value;
            OnPropertyChanged(nameof(Contacts));
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

    private ChatViewModel? _chatViewModel;
    public ChatViewModel? ChatViewModel
    {
        get => _chatViewModel;
        set
        {
            _chatViewModel = value;
            OnPropertyChanged(nameof(ChatViewModel));
        }
    }

    private ContactModel? _selectedUser;
    public ContactModel? SelectedUser
    {
        get => _selectedUser;
        set
        {
            _selectedUser = value;
            OnPropertyChanged(nameof(SelectedUser));
            ChatViewModel = new ChatViewModel(_selectedUser, _httpClient);
        }
    }
    public ICommand GetAllUsersQuery { get; }
    public ICommand GetUserByEmailQuery { get; }

    public HomeViewModel(UserStore userStore, HttpClient httpClient)
    {
        _userStore = userStore;
        _httpClient = httpClient;

        GetAllUsersQuery = new GetAllUsersQuery(this, _userStore, httpClient);
        GetUserByEmailQuery = new GetUserByEmailQuery(this, _userStore, httpClient);
    }
}