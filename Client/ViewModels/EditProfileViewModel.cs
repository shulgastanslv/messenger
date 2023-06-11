using System.Net.Http;
using System.Windows.Input;
using Client.Commands;
using Client.Services;
using Client.Stores;

namespace Client.ViewModels;

public class EditProfileViewModel : ViewModelBase
{
    private readonly NavigationStore _editProfileNavigationStore = new();
    private UserStore _userStore;

    public EditProfileViewModel(UserStore userStore, HttpClient httpClient, NavigationStore navigationStore)
    {
        _userStore = userStore;

        NavigateToSettingsCommand = new NavigateCommand<SettingsViewModel>(new NavigationService<SettingsViewModel>(
            navigationStore, () => new SettingsViewModel(userStore, httpClient, navigationStore)));


        NavigateToEditUserNameCommand = new NavigateCommand<EditUserNameViewModel>(
            new NavigationService<EditUserNameViewModel>(
                _editProfileNavigationStore,
                () => new EditUserNameViewModel(userStore, httpClient, _editProfileNavigationStore)));

        _editProfileNavigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
    }

    public ICommand NavigateToSettingsCommand { get; }
    public ICommand NavigateToEditUserNameCommand { get; }


    public UserStore UserStore
    {
        get => _userStore;
        set
        {
            _userStore = value;
            OnPropertyChanged(nameof(UserStore));
        }
    }

    public bool IsEditProfileModalOpen => _editProfileNavigationStore.IsOpen;
    public ViewModelBase? EditProfileModal => _editProfileNavigationStore.CurrentViewModel;

    private void OnCurrentViewModelChanged()
    {
        OnPropertyChanged(nameof(EditProfileModal));
        OnPropertyChanged(nameof(IsEditProfileModalOpen));
    }
}