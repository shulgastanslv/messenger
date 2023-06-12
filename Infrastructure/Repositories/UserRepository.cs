using Domain.Entities.Users;
using Domain.Primitives.Maybe;
using Domain.Primitives.Result;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<User?>> CreateUserAsync(User user, CancellationToken cancellationToken)
    {
        await _context.Users.AddAsync(user, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(user)!;
    }


    public async Task<Maybe<User?>> GetUserByUserNameAsync(string username, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(i => i.UserName == username, cancellationToken);

        return user;
    }

    public async Task<Result<User?>> UpdateUserAsync(User user, CancellationToken cancellationToken)
    {
        _context.Users.Update(user);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(user)!;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        var users = await _context.Users.ToListAsync();

        return users;
    }

    public async Task<Maybe<User?>> GetUserByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(id);

        return user!;
    }
}