using Domain.Entities;
using System.Data;
using Domain.Shared;
using Application.Common.Interfaces;
using Ardalis.GuardClauses;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<User>> CreateUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return Result<User>.Success(user);
    }

    public async Task<bool> AuthenticateUserAsync(string requestEmail, string requestPassword)
    {
        var user = await _context.Users.FirstOrDefaultAsync(i => i.Email == requestEmail && i.Password == requestPassword);

        return user != null;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        var users = await _context.Users.ToListAsync();

        return users;
    }

    public async Task<User> GetUserByIdAsync(string id)
    {
        var user = await _context.Users.FindAsync(id);

        return user!;
    }

    public async Task<string?> GetUserNameAsync(string id)
    {
        var user = await _context.Users.FirstAsync(i => i.Id == id);

        return user.UserName;
    }
}