using System;
using Client.Interfaces;
using Client.ViewModels;

namespace Client.Services;

public class NavigationService : ViewModelBase, INavigationService
{
    private readonly Func<Type, ViewModel> _viewFactory;
    private ViewModel _currentView;

    public ViewModel CurrentView
    {
        get => _currentView;
        private set
        {
            _currentView = value;
            OnPropertyChanged();

        }
    }

    public NavigationService(Func<Type, ViewModel> viewFactory)
    {
        _viewFactory = viewFactory;
    }


    public void NavigateTo<TViewModel>() where TViewModel : ViewModel
    {
       ViewModel viewModel = _viewFactory.Invoke(typeof(TViewModel));
       CurrentView = viewModel;
    }
}