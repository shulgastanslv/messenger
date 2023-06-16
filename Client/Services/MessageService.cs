using Client.Models;
using Client.Properties;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Client.Stores;

namespace Client.Services;

public static class MessageService
{
    public static async Task<ObservableCollection<MessageModel>?> LoadLocalMessagesAsync(
        ContactModel contact, CancellationToken cancellationToken)
    {
        if (contact?.ChatId == null)
            return null;

        var directoryPath = Path.Combine(
            Settings.Default.MessagesDataPath,
            contact.ChatId.ToString()!);

        Directory.CreateDirectory(directoryPath);

        var fileNames = Directory.GetFiles(directoryPath);

        ObservableCollection<MessageModel> messages = new();

        foreach (var fileName in fileNames)
        {
            var json = await File.ReadAllTextAsync(fileName, cancellationToken);
            var message = JsonConvert.DeserializeObject<MessageModel>(json);
            if (message == null) continue;

            messages.Add(message);
        }

        return messages;
    }

    public static async Task<ObservableCollection<MessageModel>?> SaveMessagesAsync(
        ContactModel contact, HttpClient httpClient, CancellationToken cancellationToken)
    {
        var content = new StringContent(
            JsonConvert.SerializeObject(contact),
            Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync("/message/get", content, cancellationToken);

        if (!response.IsSuccessStatusCode) return null;

        var messages = await response.Content.ReadAsAsync<ObservableCollection<MessageModel>>(cancellationToken);

        if (!contact.ChatId.HasValue) return null;

        await SaveEntityModelService.SaveMessagesAsync(messages, cancellationToken);

        var files = await response.Content.ReadAsAsync<ObservableCollection<MediaModel>>(cancellationToken);

        await SaveEntityModelService.SaveFilesAsync(files, cancellationToken);

        return messages;
    }

    public static async Task<ObservableCollection<MediaModel>?> SaveFilesAsync(
        ContactModel contact, HttpClient httpClient, CancellationToken cancellationToken)
    {
        var content = new StringContent(
            JsonConvert.SerializeObject(contact),
            Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync("/message/get", content, cancellationToken);

        if (!response.IsSuccessStatusCode) return null;

        var messages = await response.Content
            .ReadAsAsync<ObservableCollection<MediaModel>>(cancellationToken);

        if (!contact.ChatId.HasValue) return null;

        await SaveEntityModelService.SaveFilesAsync(messages, cancellationToken);

        return messages;
    }

    public static async Task<ObservableCollection<MediaModel>?> LoadLocalFilesAsync(
        ContactModel contact, CancellationToken cancellationToken)
    {
        if (contact?.ChatId == null)
            return null;

        var directoryPath = Path.Combine(
            Settings.Default.FilesDataPath,
            contact.ChatId.ToString()!);

        Directory.CreateDirectory(directoryPath);

        var fileNames = Directory.GetFiles(directoryPath);

        ObservableCollection<MediaModel> files = new();

        foreach (var fileName in fileNames)
        {
            var data = await File.ReadAllBytesAsync(fileName, cancellationToken);

            var file = new MediaModel
            {
                FileData = data,
                ReceiverChatId = contact.ChatId.Value,
                FileName = fileName,
            };

            if (file == null) continue;

            files.Add(file);
        }

        return files;
    }

}