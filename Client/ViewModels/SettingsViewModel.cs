using System.Net.Http;
using System.Windows.Input;
using Client.Commands;
using Client.Services;
using Client.Stores;

namespace Client.ViewModels;

public class SettingsViewModel : ViewModelBase
{
    private UserStore _userStore;

    public SettingsViewModel(HomeViewModel homeViewModel, UserStore userStore, HttpClient httpClient,
        NavigationStore navigationStore)
    {
        _userStore = userStore;

        NavigateToHomeCommand = new NavigateCommand<SettingsViewModel>(new NavigationService<SettingsViewModel>(
            navigationStore, () => null));

        NavigateToEditProfileCommand = new NavigateCommand<EditProfileViewModel>(
            new NavigationService<EditProfileViewModel>(
                navigationStore,
                () => new EditProfileViewModel(homeViewModel, userStore, httpClient, navigationStore)));
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

    public ICommand NavigateToHomeCommand { get; }
    public ICommand NavigateToEditProfileCommand { get; }
}