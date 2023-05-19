using Domain.Entities;
using Domain.Shared;

namespace Application.Common.Interfaces;

public interface IUserRepository
{
    Task<Result<User>> CreateUserAsync(User user);
    Task<bool> AuthenticateUserAsync(string requestEmail, string requestPassword);
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User> GetUserByIdAsync(string id);
    Task<string?> GetUserNameAsync(string id);
}
