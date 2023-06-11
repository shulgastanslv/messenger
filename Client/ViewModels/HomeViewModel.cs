using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows.Input;
using Client.Commands;
using Client.Models;
using Client.Queries.Users;
using Client.Services;
using Client.Stores;

namespace Client.ViewModels;

public class HomeViewModel : ViewModelBase
{
    private readonly HttpClient _httpClient;
    private readonly NavigationStore _menuNavigationStore = new();

    private ChatViewModel? _chatViewModel;

    private ObservableCollection<ContactModel> _contacts = new();

    private bool _isLoading;

    private bool _isSelectedUser;
    public bool IsModalViewModelOpen => _menuNavigationStore.IsOpen;

    private ContactModel? _selectedUser;

    private UserStore _userStore;

    public HomeViewModel(UserStore userStore, HttpClient httpClient)
    {
        _userStore = userStore;
        _httpClient = httpClient;

        GetAllUsersQuery = new GetAllUsersQuery(this, _userStore, httpClient);
        GetAllUsersQuery.Execute(null);
        GetUserByUserNameQuery = new GetUserByUserNameQuery(this, _userStore, httpClient);
        GetUserByUserNameQuery.Execute(null);

        NavigateToSettingsCommand = new NavigateCommand<SettingsViewModel>(new NavigationService<SettingsViewModel>(
            _menuNavigationStore, () => new SettingsViewModel(_userStore, httpClient,
                _menuNavigationStore)));

        _menuNavigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
    }

    public UserStore UserStore
    {
        get => _userStore;
        set
        {
            _userStore = value;
            OnPropertyChanged(nameof(UserStore));
        }
    }

    public ObservableCollection<ContactModel> Contacts
    {
        get => _contacts;
        set
        {
            _contacts = value;
            OnPropertyChanged(nameof(Contacts));
        }
    }

    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            _isLoading = value;
            OnPropertyChanged(nameof(IsLoading));
        }
    }

    public ChatViewModel? ChatViewModel
    {
        get => _chatViewModel;
        set
        {
            _chatViewModel = value;
            OnPropertyChanged(nameof(ChatViewModel));
        }
    }

    public ViewModelBase? ModalViewModel => _menuNavigationStore.CurrentViewModel;

    public ContactModel? SelectedUser
    {
        get => _selectedUser;
        set
        {
            _selectedUser = value;
            IsSelectedUser = true;
            OnPropertyChanged(nameof(SelectedUser));
            ChatViewModel = new ChatViewModel(_userStore, _selectedUser!, _httpClient);
        }
    }

    public bool IsSelectedUser
    {
        get => _isSelectedUser;
        set
        {
            _isSelectedUser = value;
            OnPropertyChanged(nameof(IsSelectedUser));
        }
    }

    public ICommand GetAllUsersQuery { get; }
    public ICommand GetUserByUserNameQuery { get; }
    public ICommand NavigateToSettingsCommand { get; }

    private void OnCurrentViewModelChanged()
    {
        OnPropertyChanged(nameof(ModalViewModel));
        OnPropertyChanged(nameof(IsModalViewModelOpen));
    }
}