using Domain.Entities;
using Domain.Primitives.Maybe;
using Domain.Primitives.Result;

namespace Application.Common.Interfaces;

public interface IUserRepository
{
    Task<Result<User?>> CreateUserAsync(User user, CancellationToken cancellationToken);
    Task<Maybe<User?>> AuthenticateUserAsync(string requestEmail, string requestPassword, CancellationToken cancellationToken);
    Task<Maybe<User?>> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<User>> GetAllUsersAsync();
}
