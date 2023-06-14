using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Client.Models;
using Client.Queries;
using Client.Stores;

namespace Client.ViewModels;

public class HomeViewModel : ViewModelBase
{
    private readonly HttpClient _httpClient;
    private ChatViewModel? _chatViewModel;

    private ObservableCollection<ContactModel> _contacts = new();

    private bool _isLoading;

    private string? _searchText;

    private bool _isSelectedUser;

    private ContactModel _selectedContact;

    private UserStore _userStore;

    public HomeViewModel(UserStore userStore, HttpClient httpClient)
    {
        _userStore = userStore;
        _httpClient = httpClient;
        _selectedContact = new ContactModel(Guid.Empty, string.Empty, null);

        GetLastMessagesQuery = new GetLastMessagesQuery(httpClient, userStore);
        GetUsersQuery = new GetUsersQuery(this, httpClient, userStore);
        SearchUserQuery = new GetUsersByUsernameQuery(this, httpClient);

        GetUsersQuery.Execute(null);

        Task.Run(GetLastMessages);
    }

    private async Task GetLastMessages()
    {
        while (true)
        {
            GetLastMessagesQuery.Execute(null);
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

            if (_searchText == string.Empty)
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
    public ICommand SearchUserQuery { get; }
    public ICommand GetLastMessagesQuery { get; }
}