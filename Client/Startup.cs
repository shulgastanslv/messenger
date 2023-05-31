using System;
using System.Net.Http;
using Client.Models;
using Client.Stores;
using Client.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Client;

public class Startup
{
    public static IServiceProvider Configure()
    {
        var services = new ServiceCollection();

        services.AddSingleton<MainViewModel>();
        services.AddSingleton<AuthenticationViewModel>();
        services.AddSingleton<RegistrationViewModel>();

        services.AddSingleton(provider => new MainWindow
        {
            DataContext = provider.GetRequiredService<MainViewModel>()
        });

        services.AddSingleton(new HttpClient
        {
            Timeout = TimeSpan.FromMilliseconds(System.Threading.Timeout.Infinite),
            BaseAddress = new Uri("https://localhost:7289")
        });

        services.AddSingleton<NavigationStore>();

        services.AddSingleton(new UserStore
        {
            User = new UserModel()
            {
                UserName = "",
                Email = "",
                Password = "",
            }
        });

        return services.BuildServiceProvider();
    }
}