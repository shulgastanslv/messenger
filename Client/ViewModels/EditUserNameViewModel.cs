using System.Net.Http;
using System.Windows.Input;
using Client.Commands;
using Client.Services;
using Client.Stores;

namespace Client.ViewModels;

public class EditUserNameViewModel : ViewModelBase
{
    public EditUserNameViewModel(UserStore userStore, HttpClient httpClient, NavigationStore editProfileNavigationStore)
    {
        NavigateToEditProfileCommand = new NavigateCommand<EditUserNameViewModel>(
            new NavigationService<EditUserNameViewModel>(
                editProfileNavigationStore, () => null));
    }

    public ICommand NavigateToEditProfileCommand { get; }
}