using System.Collections.Generic;
using Client.Stores;
using System.Net.Http;
using System.Windows.Input;
using Client.Models;
using Client.Queries;
using System.Linq;
using Client.Commands;

namespace Client.ViewModels;

public class HomeViewModel : ViewModelBase
{
    private readonly UserStore _userStore;

    private readonly NavigationStore _navigationStore = new();

    public UserStore UserStore => _userStore;

    private List<UserModel> _users = new ();

    private string _searchText;
    public string SearchText
    {
        get => _searchText;
        set
        {
            _searchText = value;
            OnPropertyChanged(nameof(SearchText));
            OnPropertyChanged(nameof(Users));
        }
    }
    public List<UserModel> Users
    {
        get
        {
            if(string.IsNullOrEmpty(SearchText))
                return _users;

            return _users.Where(u => u.UserName.Contains(SearchText)).ToList();
        }
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

    public UserModel _selectedUser;
    public UserModel SelectedUser
    {
        get => _selectedUser;
        set
        {
            _selectedUser = value;
            OnPropertyChanged(nameof(SelectedUser));
            _navigationStore.CurrentViewModel = new ChatViewModel(SelectedUser);
        }
    }

    public ICommand GetAllUsersCommand { get; }

    public HomeViewModel(UserStore userStore, HttpClient httpClient)
    { 
        _userStore = userStore;

        GetAllUsersCommand = new GetAllUsersQuery(this, userStore, httpClient);

        GetAllUsersCommand.Execute(null);

        _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
    }

    private void OnCurrentViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentChatViewModel));
    }

    public ViewModelBase CurrentChatViewModel => _navigationStore.CurrentViewModel;
}