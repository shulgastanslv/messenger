using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Application.Users.Queries.GetUsersByUsername;
using Client.Commands;
using Client.Commands.Messages;
using Client.Commands.Navigation;
using Client.Commands.Users;
using Client.Interfaces;
using Client.Models;
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

    public HomeViewModel(UserStore userStore, HttpClient httpClient,
        INavigationService createGroupNavigationService,
        INavigationService authenticationNavigationService)
    {
        _userStore = userStore;
        _httpClient = httpClient;

        GetLastMessagesCommand = new GetLastMessagesCommand(httpClient, userStore);
        LogoutCommand = new LogoutCommand(userStore, authenticationNavigationService);
        GetUsersQuery = new GetUsersQuery(this, httpClient, userStore);
        GetUsersQuery.Execute(null);
        SearchUserQuery = new GetUsersByUsernameCommand(this, httpClient);
        CreateGroupCommand = new NavigateCommand(createGroupNavigationService);


        Task.Run(GetLastMessages);
        Task.Run(UpdateUsers);
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

    private async Task UpdateUsers()
    {
        while (true)
        {
            if (SearchText == null && IsSelectedUser == false)
            {
                GetUsersQuery.Execute(null);
                await Task.Delay(5000);
            }
        }
    }

    private async Task GetLastMessages()
    {
        while (true)
        {
            GetLastMessagesCommand.Execute(null);
            await Task.Delay(1000);
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
    public ICommand SearchUserQuery { get; }
    public ICommand CreateGroupCommand { get; }
    public ICommand LogoutCommand { get; }
    public ICommand GetLastMessagesCommand { get; }
}