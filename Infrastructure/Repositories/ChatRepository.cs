using Application.Common.Abstractions;
using Domain.Entities.Chats;
using Domain.Primitives.Result;
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

    public async Task<Chat?> GetChatByUsersIdAsync(Guid user1, Guid user2, CancellationToken cancellationToken)
    {
        var chat = await _applicationDbContext.Chats.FirstOrDefaultAsync(
            c => c.Sender.Id == user1 && c.Receiver.Id == user2,
            cancellationToken);

        return chat;
    }

    public async Task<Chat?> GetChatByIdAsync(Guid chatId, CancellationToken cancellationToken)
    {
        var chat = await _applicationDbContext.Chats.SingleOrDefaultAsync(c => c.ChatId == chatId, cancellationToken);

        return chat;
    }
}