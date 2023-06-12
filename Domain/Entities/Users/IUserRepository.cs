using Domain.Primitives.Maybe;
using Domain.Primitives.Result;

namespace Domain.Entities.Users;

public interface IUserRepository
{
    Task<Result<User?>> CreateUserAsync(User user, CancellationToken cancellationToken);
    Task<Maybe<User?>> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Maybe<User?>> GetUserByUserNameAsync(string username, CancellationToken cancellationToken);
    Task<Result<User?>> UpdateUserAsync(User user, CancellationToken cancellationToken);
    Task<IEnumerable<User>> GetAllUsersAsync();
}