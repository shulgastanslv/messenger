using Domain.Entities;
using Application.Common.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Domain.Primitives.Result;
using Domain.Primitives.Maybe;

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

    public async Task<Maybe<User?>> AuthenticateUserAsync(string requestEmail, string requestPassword, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(i => i.Email == requestEmail && i.Password == requestPassword,
            cancellationToken: cancellationToken);

        return user;
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