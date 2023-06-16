using System.Collections.Generic;
using Client.Interfaces;

namespace Client.Services;

public class CompositeNavigationService : INavigationService
{
    private readonly IEnumerable<INavigationService> _navigationServices;

    public CompositeNavigationService(params INavigationService[] navigationServices)
    {
        _navigationServices = navigationServices;
    }

    public void Navigate()
    {
        foreach (var navigationService in _navigationServices)
        {
            navigationService.Navigate();
        }
    }
}