using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Client.Models;
using Client.Properties;
using Newtonsoft.Json;

namespace Client.Services;

public class SaveEntityModelService
{
    public static event EventHandler? MessagesSaved;
    public static event EventHandler? FilesSaved;
    private static readonly object _messageLock = new();
    private static readonly object _contactLock = new();
    private static readonly object _fileLock = new();


    public static void SaveEntity(MessageModel message)
    {
        var directoryPath = Path.Combine(
            Settings.Default.MessagesDataPath,
            message.ReceiverChatId.ToString());

        Directory.CreateDirectory(directoryPath);

        var json = JsonConvert.SerializeObject(message);

        var encoding = Encoding.UTF8;

        var filePath = Path.Combine(directoryPath, $"{message.Id}.json");

        lock (_messageLock)
        {
            File.WriteAllText(filePath, json, encoding);
        }
    }
    public static void SaveEntity(ContactModel contact)
    {
        var directoryPath = Path.Combine(
            Settings.Default.UserDataPath);

        Directory.CreateDirectory(directoryPath);

        var json = JsonConvert.SerializeObject(contact);

        var encoding = Encoding.UTF8;

        var filePath = Path.Combine(directoryPath, $"{contact.Id}.json");

        lock (_contactLock)
        {
            File.WriteAllText(filePath, json, encoding);
        }

    }
    public static void SaveEntity(MediaModel file)
    {
        var directoryPath = Path.Combine(
            Settings.Default.FilesDataPath);

        Directory.CreateDirectory(directoryPath);

        var json = JsonConvert.SerializeObject(file);

        var encoding = Encoding.UTF8;

        var filePath = Path.Combine(directoryPath, $"{file.Id}.json");

        lock (_fileLock)
        {
            File.WriteAllText(filePath, json, encoding);
        }

    }

    public static async Task SaveEntityAsync(MessageModel message, CancellationToken cancellationToken)
    {
        var directoryPath = Path.Combine(
            Settings.Default.MessagesDataPath,
            message.ReceiverChatId.ToString());

        Directory.CreateDirectory(directoryPath);

        var json = JsonConvert.SerializeObject(message);

        var encoding = Encoding.UTF8;

        var filePath = Path.Combine(directoryPath, $"{message.Id}.json");


        await File.WriteAllTextAsync(filePath, json, encoding, cancellationToken);
    }
    public static async Task SaveEntityAsync(ContactModel contact, CancellationToken cancellationToken)
    {
        var directoryPath = Path.Combine(
            Settings.Default.UserDataPath);

        Directory.CreateDirectory(directoryPath);

        var json = JsonConvert.SerializeObject(contact);

        var encoding = Encoding.UTF8;

        var filePath = Path.Combine(directoryPath, $"{contact.Id}.json");

        await File.WriteAllTextAsync(filePath, json, encoding, cancellationToken);
    }
    public static async Task SaveEntityAsync(MediaModel file, CancellationToken cancellationToken)
    {
        var directoryPath = Path.Combine(
            Settings.Default.FilesDataPath, file.ReceiverChatId.ToString());

        Directory.CreateDirectory(directoryPath);

        var filePath = Path.Combine(directoryPath, $"{file.FileName}");

        await File.WriteAllBytesAsync(filePath, file.FileData, cancellationToken);

    }

    public static async Task SaveMessagesAsync(IEnumerable<MessageModel> messages, CancellationToken cancellationToken)
    {
        foreach (var message in messages)
        {
            await SaveEntityAsync(message, cancellationToken);
        }

        OnMessagesSaved(EventArgs.Empty);
    }
    public static async Task SaveFilesAsync(IEnumerable<MediaModel> files, CancellationToken cancellationToken)
    {
        foreach (var file in files)
        {
            await SaveEntityAsync(file, cancellationToken);
        }

        OnFilesSaved(EventArgs.Empty);
    }

    public static void SaveFiles(IEnumerable<MediaModel> files, CancellationToken cancellationToken)
    {
        foreach (var file in files)
        {
            SaveEntity(file);
        }

        OnFilesSaved(EventArgs.Empty);
    }
    public static void SaveMessages(IEnumerable<MessageModel> messages)
    {
        foreach (var message in messages)
        {
            SaveEntity(message);
        }

        OnMessagesSaved(EventArgs.Empty);
    }

    private static void OnFilesSaved(EventArgs e)
    {
        FilesSaved?.Invoke(null, e);
    }
    protected static void OnMessagesSaved(EventArgs e)
    {
        MessagesSaved?.Invoke(null, e);
    }
}