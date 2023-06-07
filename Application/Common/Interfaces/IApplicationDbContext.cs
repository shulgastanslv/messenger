using Domain.Entities.Chats;
using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Chat> Chats { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}