using Application.Common.Abstractions;
using Domain.Entities.Chats;
using Domain.Entities.Messages;
using Domain.Primitives.Errors;
using Domain.Primitives.Result;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Infrastructure.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly string _filePath;

    private readonly IApplicationDbContext _applicationDbContext;

    public MessageRepository(string filePath, IApplicationDbContext applicationDbContext)
    {
        _filePath = filePath;
        _applicationDbContext = applicationDbContext;
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
    public async Task<Result<IEnumerable<Message>>> GetMessagesAsync(Guid receiver, Guid sender, CancellationToken cancellationToken)
    {
        var chat = await _applicationDbContext.Chats
            .FirstOrDefaultAsync(i => i.Sender!.Id == sender && i.Receiver!.Id == receiver, cancellationToken);

        if (chat == null)
            return Result.Failure<IEnumerable<Message>>(new Error("Chat not found"));

        var dictionaryPath = Path.Combine(_filePath, chat.ChatId.ToString());

        if (!Directory.Exists(dictionaryPath))
            return Result.Failure<IEnumerable<Message>>(new Error("Directory not found"));

        var fileNames = await Task.Run(() =>
            Directory.GetFiles(dictionaryPath), cancellationToken);

        var messages = new List<Message>();

        foreach (var file in fileNames)
        {
            string json = await File.ReadAllTextAsync(file, cancellationToken);

            var message = JsonConvert.DeserializeObject<Message>(json);

            messages.Add(message!);
        }

        chat!.LastUpdatedTime = DateTime.Now;

        await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return Result.Success<IEnumerable<Message>>(messages);
    }

    public async Task<Result<IEnumerable<Message>>> GetLastMessagesAsync(Guid receiver, Guid sender, CancellationToken cancellationToken)
    {
        var chat = await _applicationDbContext.Chats
            .FirstOrDefaultAsync(i => i.Sender!.Id == sender && i.Receiver!.Id == receiver, cancellationToken);

        var dictionaryPath = Path.Combine(_filePath, chat.ChatId.ToString());

        var fileNames = await Task.Run(() =>
            Directory.GetFiles(dictionaryPath), cancellationToken);

        var messages = new List<Message>();

        foreach (var file in fileNames)
        {
            var fileInfo = new FileInfo(file);

            DateTime lastWriteTime = fileInfo.LastWriteTime;
            string formattedTime = lastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");

            if (DateTime.Parse(formattedTime) > chat.LastUpdatedTime)
            {
                string json = await File.ReadAllTextAsync(file, cancellationToken);
                var message = JsonConvert.DeserializeObject<Message>(json);
                messages.Add(message!);
            }
        }

        chat!.LastUpdatedTime = DateTime.Now;

        await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return Result.Success<IEnumerable<Message>>(messages);
    }
}