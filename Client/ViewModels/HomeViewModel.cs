using System.Collections.Generic;
using Client.Stores;
using System.Net.Http;
using System.Windows.Input;
using Client.Commands;
using Client.Models;

namespace Client.ViewModels;

public class HomeViewModel : ViewModelBase
{
    private List<UserModel> _users = new ();

    private readonly UserStore _userStoreStore;
    public List<UserModel> Users
    {
        get => _users;
        set
        {
            _users = value;
            OnPropertyChanged(nameof(Users));
        }
    }
    public UserStore UserStore => _userStoreStore;

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

    public ICommand GetAllUsersCommand { get;  }

    public HomeViewModel(UserStore userStoreStore, HttpClient httpClient)
    {
        _userStoreStore = userStoreStore;

        GetAllUsersCommand = new GetAllUsersCommand(userStoreStore, this, httpClient);

        GetAllUsersCommand.Execute(httpClient);
    }

}