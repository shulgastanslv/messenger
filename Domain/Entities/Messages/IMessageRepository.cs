using Domain.Entities.Chats;
using Domain.Entities.Users;
using Domain.Primitives.Result;

namespace Domain.Entities.Messages;

public interface IMessageRepository
{
    Task<Result> SaveMessageAsync(Chat chat, Message message, CancellationToken cancellationToken);
    Task<Result<IEnumerable<Message>>> GetMessagesAsync(Guid receiver, Guid sender,
        CancellationToken cancellationToken);
}