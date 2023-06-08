using Domain.Primitives.Result;

namespace Domain.Entities.Chats;
public interface IChatRepository
{
    Task<Result<Chat>> CreateChatAsync(Guid user1, Guid user2,
        CancellationToken cancellationToken);

    Task<Result<Chat>> GetChatAsync(Guid sender, Guid receiver,
        CancellationToken cancellationToken);
}