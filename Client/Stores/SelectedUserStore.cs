using System;
using Client.Models;

namespace Client.Stores;

public class SelectedUserStore
{
    private UserModel _userModel;

    public UserModel UserModel
    {
        get => _userModel;
        set
        {
            _userModel = value;
            SelectedUserChanged?.Invoke();
        }
    }
    
    public event Action SelectedUserChanged;
}