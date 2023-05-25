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
    private static bool isListening = false;

    private static HttpClient httpClient;
    public static bool IsListening => isListening;
    public static HttpClient HttpClient => httpClient;

    private readonly ServiceProvider _serviceProvider;
    public App()
    {
        IServiceCollection services = new ServiceCollection();

        services.AddSingleton(provider => new MainWindow
        {
            DataContext = provider.GetRequiredService<MainViewModel>()
        });

        services.AddSingleton<SignInViewModel>();
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<SignUpViewModel>();
        services.AddSingleton<HomeViewModel>();
        services.AddSingleton<UserChatViewModel>();


        services.AddSingleton<INavigationService, NavigationService>();

        services.AddSingleton<Func<Type, ViewModel>>(serviceProvider => viewModelType => 
            (ViewModel)serviceProvider.GetRequiredService(viewModelType)); 

        _serviceProvider = services.BuildServiceProvider();

        httpClient = new HttpClient();

        httpClient.Timeout = TimeSpan.FromMilliseconds(System.Threading.Timeout.Infinite);
        httpClient.BaseAddress = new Uri("https://localhost:7289");
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();

        base.OnStartup(e);
    }
}
