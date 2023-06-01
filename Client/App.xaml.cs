using System;
using System.Windows;
using Client.Stores;
using Client.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Client;
public partial class App
{
    private readonly IServiceProvider _serviceProvider;

    public App()
    {
        _serviceProvider = Client.Startup.Configure();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        var authenticationView = _serviceProvider.GetRequiredService<AuthenticationViewModel>();

        var navigationStore = _serviceProvider.GetRequiredService<NavigationStore>();
        navigationStore.CurrentViewModel = authenticationView;


        MainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        MainWindow.Show();

        base.OnStartup(e);
    }
}
