using System;
using System.Net.Http;
using System.Threading;
using Client.Interfaces;
using Client.Services;
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
        services.AddSingleton<HomeViewModel>();
        services.AddSingleton<ChatViewModel>();

        services.AddSingleton(new HttpClient
        {
            Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite),
            BaseAddress = new Uri("https://localhost:7289")
        });

        services.AddSingleton<NavigationStore>();
        services.AddSingleton<ModalNavigationStore>();
        services.AddSingleton(UserStoreSettingsService.GetUserStore());

        services.AddSingleton(CreateWelcomeNavigationService);
        services.AddSingleton<CloseModalNavigationService>();

        services.AddSingleton<RegistrationViewModel>(s => new RegistrationViewModel(
            s.GetRequiredService<UserStore>(),
            s.GetRequiredService<HttpClient>(),
            CreateAuthenticationNavigationService(s),
            CreateHomeNavigationService(s)));


        services.AddSingleton<AuthenticationViewModel>(s => new AuthenticationViewModel(
            s.GetRequiredService<UserStore>(),
            s.GetRequiredService<HttpClient>(),
            CreateRegistrationNavigationService(s),
            CreateHomeNavigationService(s)));


        services.AddSingleton<HomeViewModel>(s => new HomeViewModel(
            s.GetRequiredService<UserStore>(),
            s.GetRequiredService<HttpClient>(),
            CreateCreateGroupNavigationService(s),
            CreateAuthenticationNavigationService(s)));

        services.AddTransient<WelcomeViewModel>(s => new WelcomeViewModel(
            s.GetRequiredService<HttpClient>(),
            s.GetRequiredService<UserStore>(),
            CreateHomeNavigationService(s),
            CreateRegistrationNavigationService(s),
            CreateAuthenticationNavigationService(s)));

        services.AddTransient(CreateCreateGroupViewModel);

        services.AddSingleton<MainViewModel>(s => new MainViewModel(
            s.GetRequiredService<NavigationStore>(),
            s.GetRequiredService<ModalNavigationStore>(),
            s.GetRequiredService<UserStore>()));


        services.AddSingleton<MainViewModel>(s => new MainViewModel(
            s.GetRequiredService<NavigationStore>(),
            s.GetRequiredService<ModalNavigationStore>(),
            s.GetRequiredService<UserStore>()));

        services.AddSingleton<MainWindow>(provider => new MainWindow(
            provider.GetRequiredService<MainViewModel>()));


        return services.BuildServiceProvider();
    }

    private static INavigationService CreateAuthenticationNavigationService(IServiceProvider serviceProvider)
    {
        return new NavigationService<AuthenticationViewModel>(
            serviceProvider.GetRequiredService<NavigationStore>(),
            serviceProvider.GetRequiredService<AuthenticationViewModel>);
    }

    private static INavigationService CreateRegistrationNavigationService(IServiceProvider serviceProvider)
    {
        return new NavigationService<RegistrationViewModel>(
            serviceProvider.GetRequiredService<NavigationStore>(),
            serviceProvider.GetRequiredService<RegistrationViewModel>);
    }

    private static INavigationService CreateHomeNavigationService(IServiceProvider serviceProvider)
    {
        return new NavigationService<HomeViewModel>(
            serviceProvider.GetRequiredService<NavigationStore>(),
            serviceProvider.GetRequiredService<HomeViewModel>);
    }

    private static INavigationService CreateWelcomeNavigationService(IServiceProvider serviceProvider)
    {
        return new NavigationService<WelcomeViewModel>(
            serviceProvider.GetRequiredService<NavigationStore>(),
            serviceProvider.GetRequiredService<WelcomeViewModel>);
    }


    private static INavigationService CreateCreateGroupNavigationService(IServiceProvider serviceProvider)
    {
        return new ModalNavigationService<CreateGroupViewModel>(
            serviceProvider.GetRequiredService<ModalNavigationStore>(),
            serviceProvider.GetRequiredService<CreateGroupViewModel>);
    }

    private static CreateGroupViewModel CreateCreateGroupViewModel(IServiceProvider serviceProvider)
    {
        var navigationService = new CompositeNavigationService(
            serviceProvider.GetRequiredService<CloseModalNavigationService>(),
            CreateHomeNavigationService(serviceProvider));

        return new CreateGroupViewModel(
            serviceProvider.GetRequiredService<HttpClient>(),
            navigationService);
    }
}