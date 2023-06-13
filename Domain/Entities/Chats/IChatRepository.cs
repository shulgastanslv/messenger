using Domain.Primitives.Result;

namespace Domain.Entities.Chats;

public interface IChatRepository
{
    Task<Result<Chat>> CreateChatAsync(Guid user1, Guid user2, CancellationToken cancellationToken);

    Task<Chat?> GetChatByUsersIdAsync(Guid user1, Guid user2, CancellationToken cancellationToken);

    Task<Chat?> GetChatByIdAsync(Guid chatId, CancellationToken cancellationToken);
}