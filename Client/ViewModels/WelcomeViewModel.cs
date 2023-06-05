using Client.Stores;
using System.Net.Http;
using System.Windows.Input;
using Client.Commands;
using Client.Services;

namespace Client.ViewModels;

public class WelcomeViewModel : ViewModelBase
{
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

    public ICommand NavigateCommand { get; }

    public WelcomeViewModel(UserStore userStore, HttpClient httpClient, NavigationStore navigationStore)
    {
        NavigateCommand = new NavigateCommand<AuthenticationViewModel>(
            new NavigationService<AuthenticationViewModel>(navigationStore,
                () => new AuthenticationViewModel(userStore, httpClient, navigationStore)));
    }
}