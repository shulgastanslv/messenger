using Domain.Entities.Contacts;
using Domain.Primitives.Result;

namespace Domain.Entities.Users;

public interface IUserRepository
{
    Task<Result<User?>> CreateAsync(User user, CancellationToken cancellationToken);
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<User?>? GetByUsernameAsync(string username, CancellationToken cancellationToken);
    Task<IEnumerable<User>> GetUsersByUsernameAsync(string username, CancellationToken cancellationToken);
    Task<Result<Contact>> ConvertToContactAsync(User user, Guid sender, CancellationToken cancellationToken);
    Task<Result<User?>> UpdateAsync(User user, CancellationToken cancellationToken);
}