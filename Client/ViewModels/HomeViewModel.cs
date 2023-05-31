using Client.Stores;
using System.Net.Http;

namespace Client.ViewModels;

public class HomeViewModel : ViewModelBase
{
    private readonly UserStore _userStore;

    public string Text
    {
        get => _userStore.User.Email;
    }

    public HomeViewModel(UserStore userStore)
    {
        _userStore = userStore;
    }

}