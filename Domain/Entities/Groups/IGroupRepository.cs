using Domain.Primitives.Result;

namespace Domain.Entities.Groups;

public interface IGroupRepository
{
    Task<Result<Group>> CreateGroupAsync(Group group, CancellationToken cancellationToken);
    Task<IEnumerable<Group>> GetGroupsByUsernameAsync(string groupName, CancellationToken cancellationToken);
}