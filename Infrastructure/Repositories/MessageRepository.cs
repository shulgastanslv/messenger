using System.Text;
using System.Text.Json;
using Domain.Entities.Chats;
using Domain.Entities.Messages;
using Domain.Entities.Users;
using Domain.Primitives.Result;

namespace Infrastructure.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly string _filePath;
    public MessageRepository(string filePath)
    {
        _filePath = filePath;
    }

    public async Task<Result> SaveMessageAsync(Chat chat, Message message, CancellationToken cancellationToken)
    {
        string directoryPath = _filePath + $"{chat.ChatId}\\";

        Directory.CreateDirectory(directoryPath);

        var jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        var json = JsonSerializer.Serialize(message, jsonOptions);

        await File.WriteAllTextAsync(_filePath + $"{chat.ChatId}\\{message.Id}.json", json, cancellationToken);

        return Result.Success();
    }
}