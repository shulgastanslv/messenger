using Client.ViewModels;

namespace Client.Interfaces;

public interface INavigationService
{
    ViewModel CurrentView { get; }
    void NavigateTo<T>() where T : ViewModel;
}


