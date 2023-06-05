using Client.Models;
using Client.Stores;
using System.Net.Http;
using System.Windows.Input;
using Client.Commands;
using Client.Services;

namespace Client.ViewModels;

public class ChatViewModel : ViewModelBase
{
    private UserModel? _userModel;
    public UserModel? UserModel
    {
        get => _userModel;
        set
        {
            _userModel = value;
            OnPropertyChanged(nameof(UserStore));
        }
    }

    public ICommand SendMessageCommand { get; }

    public ChatViewModel(UserModel? userModel)
    {
        _userModel = userModel;

        SendMessageCommand = 
    }
   
}