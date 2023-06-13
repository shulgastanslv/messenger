using System.Security.Claims;
using Domain.Entities.Users;

namespace Application.Common.Abstractions;

public interface IJwtProvider
{
    string GetJwtToken(User user);

    Task<Guid?> GetUserId(ClaimsPrincipal principal);
}