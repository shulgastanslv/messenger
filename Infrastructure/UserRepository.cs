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

    public async Task<Result<User>>? CreateUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return Result<User>.Success(user);
    }

    public bool AuthenticateUserAsync(string requestEmail, string requestPassword)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == requestEmail && u.Password == requestPassword);

        return user is not null;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        var users = await _context.Users.ToListAsync();

        return users;
    }

    public async Task<User> GetUserByIdAsync(string Id)
    {
        var user = _context.Users.FindAsync(Id)!;

        return user.Result!;
    }
}