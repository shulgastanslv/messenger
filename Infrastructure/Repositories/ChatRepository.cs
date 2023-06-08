using Application.Common.Interfaces;
using Domain.Entities.Chats;
using Domain.Primitives.Result;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ChatRepository : IChatRepository
{
    private readonly IApplicationDbContext _applicationDbContext;

    public ChatRepository(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }
    public async Task<Result<Chat>> CreateChatAsync(Guid user1, Guid user2, CancellationToken cancellationToken)
    {
        var chat = new Chat(
            Guid.NewGuid(),
            user1,
            user2);

        await _applicationDbContext.Chats.AddAsync(chat, cancellationToken);
        await _applicationDbContext.Chats.AddAsync(chat.InversedChat, cancellationToken);
        await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return Result.Success(chat);
    }

    public async Task<Result<Chat>> GetChatAsync(Guid sender, Guid receiver, CancellationToken cancellationToken)
    {
        var chat = await _applicationDbContext.Chats
            .FirstOrDefaultAsync(c =>
                    c.Sender!.Id == sender && c.Receiver!.Id == receiver,
                cancellationToken);

        if (chat == null)
        {
            return await CreateChatAsync(sender, receiver, cancellationToken);
        }

        return Result.Success(chat);
    }
}