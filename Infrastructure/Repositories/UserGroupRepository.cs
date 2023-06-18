using Application.Common.Abstractions;
using Domain.Entities.UserGroups;
using Domain.Primitives.Result;

namespace Infrastructure.Repositories;

public class UserGroupRepository : IUserGroupRepository
{
    private readonly IApplicationDbContext _context;

    public UserGroupRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<UserGroup>> AddUserGroupAsync(UserGroup userGroup, CancellationToken cancellationToken)
    {
        await _context.UserGroups.AddAsync(userGroup, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(userGroup);
    }
}