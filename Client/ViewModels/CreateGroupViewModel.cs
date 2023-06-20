using Client.Interfaces;
using System.Net.Http;
using System.Windows.Input;
using Client.Commands.Groups;
using Client.Commands.Navigation;
using Client.Services;

namespace Client.ViewModels;

public record GroupName(string Name);

public class CreateGroupViewModel : ViewModelBase
{
    private string? _groupName;

    public CreateGroupViewModel(HttpClient httpClient, INavigationService navigationService)
    {
        CreateGroupCommand = new CreateGroupCommand(httpClient, this, navigationService);
        CloseCommand = new NavigateCommand(navigationService);
    }


    public string? GroupName
    {
        get => _groupName;
        set
        {
            _groupName = value;
            OnPropertyChanged(nameof(GroupName));
        }
    }

    public ICommand CreateGroupCommand { get; }
    public ICommand CloseCommand { get; }
}