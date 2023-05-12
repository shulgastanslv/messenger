using Domain.Entities;
using Domain.Shared;

namespace Application.Common.Interfaces;

public interface IUserRepository
{
    Task<Result>? CreateUserAsync(User user);
}