using Application.Common.Abstractions;
using Domain.Entities.Contacts;
using Domain.Entities.Users;
using Domain.Primitives.Errors;
using Domain.Primitives.Result;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IApplicationDbContext _context;

    public UserRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<User?>> CreateAsync(User user, CancellationToken cancellationToken)
    {
        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(user)!;
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await _context.Users.SingleOrDefaultAsync(i => i.Id == id, cancellationToken);

        return user;
    }

    public async Task<User?>? GetByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        return await _context.Users.SingleOrDefaultAsync(i => i.Username == username, cancellationToken);
    }

    public async Task<IEnumerable<User>> GetUsersByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        return await _context.Users
            .AsNoTracking()
            .Where(u => u.Username!.StartsWith(username))
            .Include(u => u.SentChats)
            .ToListAsync(cancellationToken);
    }

    public async Task<Result<Contact>> ConvertToContactAsync(User user, Guid sender, CancellationToken cancellationToken)
    {
        var chat = await _context.Chats
            .FirstOrDefaultAsync(c =>
                    c.SenderId == sender && c.ReceiverId == user.Id,
                cancellationToken);

        if (chat == null) 
            return Result.Failure<Contact>(new Error($"Chat for contact {user.Id} doesn't exist"));

        return Result.Success(new Contact(
            user.Id,
            user.Username!,
            chat.ChatId));
    }

    public async Task<Result<User?>> UpdateAsync(User user, CancellationToken cancellationToken)
    {
        _context.Users.Update(user);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(user)!;
    }
}