using Domain.Entities.Chats;
using Domain.Primitives.Result;

namespace Domain.Entities.Messages;

public interface IMessageRepository
{
    Task<Result> SaveMessageAsync(Message message, CancellationToken cancellationToken);

    Task<Result<IEnumerable<Message>>> GetMessagesAsync(Guid chatId, CancellationToken cancellationToken);

    Task<IEnumerable<Message>?> GetLastMessagesAsync(Guid chatId, DateTime lastMessageDate,
        CancellationToken cancellationToken);
}