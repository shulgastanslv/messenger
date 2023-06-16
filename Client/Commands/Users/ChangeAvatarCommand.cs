using System.IO;
using System.Net.Http;
using System.Threading;
using Client.Services;
using Client.Stores;
using Client.ViewModels;
using Microsoft.Win32;

namespace Client.Commands.Users;

public class ChangeAvatarCommand : CommandBase
{
    private readonly HttpClient _httpClient;
    private readonly UserStore _userStore;
    private readonly NavigationStore _navigationStore;

    public ChangeAvatarCommand(HttpClient httpClient, UserStore userStore, NavigationStore navigationStore)
    {
        _httpClient = httpClient;
        _userStore = userStore;
        _navigationStore = navigationStore;
    }

    public override async void Execute(object? parameter)
    {
        var openFileDialog = new OpenFileDialog
        {
            Filter = "Все файлы (*.*)|*.*"
        };

        var result = openFileDialog.ShowDialog();

        if (result != true) return;

        var selectedFilePath = openFileDialog.FileName;

        _navigationStore.CurrentViewModel = new HomeViewModel(_userStore, _httpClient, _navigationStore);
    }
}