using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Application.Messages.Queries.GetFiles;
using Client.Commands;
using Client.Commands.Messages;
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

    private ObservableCollection<ContactModel> _contacts = new ();

    private bool _isLoading;

    private string? _searchText;

    private bool _isSelectedUser;

    private ContactModel _selectedContact;

    private UserStore _userStore;

    public HomeViewModel(UserStore userStore, HttpClient httpClient, NavigationStore navigationStore)
    {
        _userStore = userStore;
        _httpClient = httpClient;
        _selectedContact = new ContactModel(Guid.Empty, string.Empty, null);
        _isSelectedUser = false;

        LogoutCommand = new LogoutCommand(_userStore, new NavigationService<AuthenticationViewModel>(
            navigationStore, (() => new AuthenticationViewModel(new UserStore(), httpClient, navigationStore))));

        SaveUserStoreCommand = new SaveUserStoreCommand(_userStore);

        SaveUserStoreCommand.Execute(null);

        GetLastMessagesQuery = new GetLastMessagesQuery(httpClient, userStore);

        GetUsersQuery = new GetUsersQuery(this, httpClient, userStore);

        SearchUserQuery = new GetUsersByUsernameQuery(this, httpClient);

        ChangeAvatarCommand = new ChangeAvatarCommand(httpClient, _userStore, navigationStore);

        GetUsersQuery.Execute(null);

        Task.Run(GetLastMessages);
    }

    private async Task GetLastMessages()
    {
        while (true)
        {
            GetLastMessagesQuery.Execute(null);
            await Task.Delay(5000);
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
    public string Avatar
    {
        get => _userStore.User.AvatarPath;
        set
        {
            _userStore.User.AvatarPath = value;
            OnPropertyChanged(nameof(Avatar));
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
            ChatViewModel = new ChatViewModel( _userStore, _selectedContact, _httpClient);
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
    public ICommand LogoutCommand { get; }
    public ICommand GetLastMessagesQuery { get; }
    public ICommand GetFilesQuery { get; }
    public ICommand ChangeAvatarCommand { get; }
    public ICommand SaveUserStoreCommand { get; }
}