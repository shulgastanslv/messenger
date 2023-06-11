using System.Security.Claims;
using Domain.Entities.Users;
using Domain.Primitives.Maybe;

namespace Application.Common.Abstractions;

public interface IJwtProvider
{
    string GetJwtToken(User user);

    Maybe<Guid> GetUserIdAsync(ClaimsPrincipal principal);
}