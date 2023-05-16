using System;
using System.Net.Http;
using System.Windows;
using Client.Interfaces;
using Client.Services;
using Client.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Client;
public partial class App
{
    private readonly ServiceProvider _serviceProvider;
    public App()
    {
        IServiceCollection services = new ServiceCollection();

        services.AddSingleton<MainWindow>(provider => new MainWindow
        {
            DataContext = provider.GetRequiredService<MainViewModel>()
        });

        services.AddSingleton<SignInViewModel>();
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<SignUpViewModel>();
        services.AddSingleton<HomeViewModel>();
        services.AddSingleton<INavigationService, NavigationService>();

        services.AddSingleton<Func<Type, ViewModel>>(serviceProvider => viewModelType => 
            (ViewModel)serviceProvider.GetRequiredService(viewModelType));

        _serviceProvider = services.BuildServiceProvider();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();

        base.OnStartup(e);
    }
}
