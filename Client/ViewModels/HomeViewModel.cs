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
    public UserStore UserStore => _userStore;

    private List<UserModel> _users = new ();

    private bool _isPanelVisible;
    public bool IsPanelVisible
    {
        get => _isPanelVisible;
        set
        {
            _isPanelVisible = value;
            OnPropertyChanged(nameof(IsPanelVisible));
        }
    }

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

    public ICommand GetAllUsersCommand { get;  }
    public ICommand ShowPanelCommand { get;  }
    public ICommand HidePanelCommand { get;  }


    public HomeViewModel(UserStore userStore, HttpClient httpClient)
    {
        _userStore = userStore;

        _isPanelVisible = false;

        GetAllUsersCommand = new GetAllUsersQuery( this, userStore, httpClient);
        GetAllUsersCommand.Execute(null);

        ShowPanelCommand = new ShowPanelCommand(this);
        ShowPanelCommand.Execute(null);

        HidePanelCommand = new HidePanelCommand(this);
        HidePanelCommand.Execute(null);

    }
}