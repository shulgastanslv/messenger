using System.Text;
using Application.Common.Abstractions;
using Domain.Entities.Messages;
using Domain.Primitives.Result;
using Newtonsoft.Json;

namespace Infrastructure.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly string _filePath;

    public MessageRepository(string filePath, IApplicationDbContext applicationDbContext)
    {
        _filePath = filePath;
    }

    public async Task<Result> SaveMessageAsync(Message message, CancellationToken cancellationToken)
    {
        var directoryPath = Path.Combine(_filePath, message.ReceiverChatId.ToString());
        Directory.CreateDirectory(directoryPath);

        var json = JsonConvert.SerializeObject(message);

        var encoding = Encoding.UTF8;

        await File.WriteAllTextAsync(Path.Combine(directoryPath, $"{message.Id}.json"), json,
            encoding, cancellationToken);

        return Result.Success();
    }

    public async Task<Result<IEnumerable<Message>>> GetMessagesAsync(Guid chatId, CancellationToken cancellationToken)
    {
        var directoryPath = Path.Combine(_filePath, chatId.ToString());
        var fileNames = Directory.GetFiles(directoryPath);

        List<Message> messages = new();

        foreach (var fileName in fileNames)
        {
            var json = await File.ReadAllTextAsync(fileName, cancellationToken);
            var message = JsonConvert.DeserializeObject<Message>(json);
            if (message == null) continue;

            messages.Add(message);
        }

        return Result.Success<IEnumerable<Message>>(messages);
    }

    public async Task<IEnumerable<Message>?> GetLastMessagesAsync(Guid chatId, DateTime lastMessageDate,
        CancellationToken cancellationToken)
    {
        var directoryPath = Path.Combine(_filePath, chatId.ToString());
        var lastModified = Directory.GetLastWriteTime(directoryPath);

        if (lastModified <= lastMessageDate) return null;

        var files = Directory.GetFiles(directoryPath);
        var messages = new List<Message>();

        foreach (var file in files)
        {
            var creationTime = File.GetCreationTime(file);

            if (creationTime > lastMessageDate)
            {
                var json = await File.ReadAllTextAsync(file, cancellationToken);
                var message = JsonConvert.DeserializeObject<Message>(json);
                if (message == null) continue;

                messages.Add(message);
            }
        }

        return messages;
    }

    public async Task<Result<IEnumerable<Media>>> GetFilesAsync(Guid chatId, CancellationToken cancellationToken)
    {
        var directoryPath = Path.Combine(_filePath, chatId.ToString());
        var fileNames = Directory.GetFiles(directoryPath);

        List<Media> files = new();

        foreach (var fileName in fileNames)
        {
            var json = await File.ReadAllTextAsync(fileName, cancellationToken);
            var file = JsonConvert.DeserializeObject<Media>(json);

            if (file == null || file.FileData == null
                             || file.FileName == null) continue;

            files.Add(file);
        }

        return Result.Success<IEnumerable<Media>>(files);
    }
}