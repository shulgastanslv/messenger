using System.Net.Http;
using System.Windows.Input;
using Client.Commands;
using Client.Services;
using Client.Stores;

namespace Client.ViewModels;

public class EditUserNameViewModel : ViewModelBase
{
    private string _userName;

    public EditUserNameViewModel(HomeViewModel homeViewModel, UserStore userStore, HttpClient httpClient,
        NavigationStore editProfileNavigationStore)
    {
        userStore.User.UserName = "хуй";

        NavigateToEditProfileCommand = new NavigateCommand<EditUserNameViewModel>(
            new NavigationService<EditUserNameViewModel>(
                editProfileNavigationStore, () => null));
    }

    public ICommand NavigateToEditProfileCommand { get; }

    public string UserName
    {
        get => _userName;
        set
        {
            _userName = value;
            OnPropertyChanged(nameof(UserName));
        }
    }
}