using Client.Models;
using Client.Stores;
using System.Net.Http;

namespace Client.ViewModels;

public class ChatViewModel : ViewModelBase
{
    private UserModel? _userModel;

    public ChatViewModel(UserModel? userModel)
    {
        _userModel = userModel;
    }

    public UserModel? UserModel
    {
        get => _userModel;
        set
        {
            _userModel = value;
            OnPropertyChanged(nameof(UserStore));
        }
    }

   
}