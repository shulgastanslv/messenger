using Application.Common.Abstractions;
using Domain.Entities.Groups;
using Domain.Primitives.Result;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class GroupRepository : IGroupRepository
{
    private readonly IApplicationDbContext _context;

    public GroupRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Group>> CreateGroupAsync(Group group, CancellationToken cancellationToken)
    {
        await _context.Groups.AddAsync(group, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(group);
    }

    public async Task<IEnumerable<Group>> GetGroupsByUsernameAsync(string groupName,
        CancellationToken cancellationToken)
    {
        var groups = await _context.Groups
            .AsNoTracking()
            .Where(g => g.Name.StartsWith(groupName))
            .Include(g => g.UserGroups)
            .ToListAsync(cancellationToken);

        return groups;
    }
}