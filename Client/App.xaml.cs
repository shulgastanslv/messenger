using System;
using System.Net.Http;
using System.Threading;
using System.Windows;
using Azure;
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
        var startPage = _serviceProvider.GetRequiredService<WelcomeViewModel>();

        var navigationStore = _serviceProvider.GetRequiredService<NavigationStore>();
        navigationStore.CurrentViewModel = startPage;

        MainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        MainWindow.Show();

        base.OnStartup(e);
    }
}
