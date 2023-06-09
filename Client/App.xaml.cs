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

    protected override async void OnStartup(StartupEventArgs e)
    {
        ViewModelBase viewModel = _serviceProvider.GetRequiredService<HomeViewModel>(); ;

        if (string.IsNullOrEmpty(Client.Properties.Settings.Default.Token))
        {
            viewModel = _serviceProvider.GetRequiredService<RegistrationViewModel>();
        }
        else
        {
            var httpClient = _serviceProvider.GetRequiredService<HttpClient>();
            var response = await httpClient.PostAsync("/authentication/confirm", null);

            if (!response.IsSuccessStatusCode)
            {
                viewModel = _serviceProvider.GetRequiredService<HomeViewModel>();
            }
        }


        var navigationStore = _serviceProvider.GetRequiredService<NavigationStore>();
        navigationStore.CurrentViewModel = viewModel;

        MainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        MainWindow.Show();

        base.OnStartup(e);
    }
}
