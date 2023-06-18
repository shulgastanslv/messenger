using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows.Input;
using Client.Commands.Messages;
using Client.Models;
using Client.Queries;
using Client.Services;
using Client.Stores;

namespace Client.ViewModels;

public class ChatViewModel : ViewModelBase
{
    private ContactModel _currentContact;

    private ObservableCollection<MessageModel> _messages = new();

    private string _messageText;

    public ChatViewModel(UserStore userStore, ContactModel currentContact, HttpClient httpClient)
    {
        _currentContact = currentContact;

        UserStore = userStore;

        SendMessageCommand = new SendMessageCommand(this, CurrentContact, httpClient);

        GetMessagesQuery = new LoadMessagesCommand(this, httpClient);

        GetLastMessagesQuery = new GetLastMessagesQuery(httpClient, UserStore);

        GetMessagesQuery.Execute(null);

        SendMediaCommand = new SendMediaCommand(httpClient, _currentContact, this);

        SaveMediaCommand = new SaveMediaCommand(this);

        SaveEntityModelService.MessagesSaved += (sender, args) => { GetMessagesQuery.Execute(null); };
    }

    public UserStore UserStore { get; }
    public ContactModel CurrentContact
    {
        get => _currentContact;
        set
        {
            _currentContact = value;
            OnPropertyChanged(nameof(CurrentContact));
        }
    }

    private MessageModel? _selectedMessage;

    public MessageModel? SelectedMessage
    {
        get => _selectedMessage;
        set
        {
            _selectedMessage = value;
            OnPropertyChanged(nameof(SelectedMessage));
            SaveMediaCommand.Execute(null);
        }
    }

    public ObservableCollection<MessageModel> Messages
    {
        get => _messages;
        set
        {
            _messages = value;
            OnPropertyChanged(nameof(Messages));
        }
    }

    public string MessageText
    {
        get => _messageText;
        set
        {
            _messageText = value;
            OnPropertyChanged(nameof(MessageText));
        }
    }

    public ICommand GetMessagesQuery { get; }
    public ICommand SendMessageCommand { get; }
    public ICommand GetLastMessagesQuery { get; }
    public ICommand SendMediaCommand { get; }
    public ICommand SaveMediaCommand { get; }
}