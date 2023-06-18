using Domain.Entities.Chats;
using Domain.Entities.Groups;
using Domain.Entities.UserGroups;
using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Abstractions;

public interface IApplicationDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<UserGroup> UserGroups { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}