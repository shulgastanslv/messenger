using Domain.Primitives.Result;

namespace Domain.Entities.UserGroups;

public interface IUserGroupRepository
{
    Task<Result<UserGroup>> AddUserGroupAsync(UserGroup userGroup, CancellationToken cancellationToken);
}