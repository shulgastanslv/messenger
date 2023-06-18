using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Client.Commands;
using Client.Commands.Users;
using Client.Models;
using Client.Queries;
using Client.Services;
using Client.Stores;

namespace Client.ViewModels;

public class HomeViewModel : ViewModelBase
{
    private readonly HttpClient _httpClient;

    private ChatViewModel? _chatViewModel;

    private ObservableCollection<ContactModel> _contacts = new();

    private bool _isLoading;

    private bool _isSelectedUser;

    private string? _searchText;

    private ContactModel _selectedContact;

    private UserStore _userStore;

    public HomeViewModel(UserStore userStore, HttpClient httpClient, NavigationStore navigationStore)
    {
        _userStore = userStore;
        _httpClient = httpClient;
        _selectedContact = new ContactModel(Guid.Empty, string.Empty, null);
        _isSelectedUser = false;

        LogoutCommand = new LogoutCommand(_userStore, new NavigationService<AuthenticationViewModel>(
            navigationStore, () => new AuthenticationViewModel(new UserStore(), httpClient, navigationStore)));

        GetLastMessagesCommand = new GetLastMessagesQuery(httpClient, userStore);

        SaveUserStoreCommand = new SaveUserStoreCommand(_userStore);

        SaveUserStoreCommand.Execute(null);

        GetUsersQuery = new GetUsersQuery(this, httpClient, userStore);

        SearchUserQuery = new GetUsersByUsernameQuery(this, httpClient);

        GetUsersQuery.Execute(null);

        Task.Run(GetLastMessages);
    }

    private async Task GetLastMessages()
    {
        while (true)
        {
            GetLastMessagesCommand.Execute(null);
            await Task.Delay(1000);
        }
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

    public ContactModel SelectedContact
    {
        get => _selectedContact;
        set
        {
            _selectedContact = value;
            IsSelectedUser = true;
            OnPropertyChanged(nameof(SelectedContact));
            ChatViewModel = new ChatViewModel(_userStore, _selectedContact, _httpClient);
        }
    }

    public string? SearchText
    {
        get => _searchText;
        set
        {
            _searchText = value;

            SearchUserQuery.Execute(null);

            OnPropertyChanged(nameof(SearchText));

            if (value == "")
                GetUsersQuery.Execute(null);
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

    public ICommand GetUsersQuery { get; }
    public ICommand GetLastMessagesCommand { get; }
    public ICommand SearchUserQuery { get; }
    public ICommand LogoutCommand { get; }
    public ICommand SaveUserStoreCommand { get; }
}