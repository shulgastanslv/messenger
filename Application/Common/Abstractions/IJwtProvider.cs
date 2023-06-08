using Domain.Entities.Users;
using Domain.Primitives.Maybe;
using System.Security.Claims;

namespace Application.Common.Abstractions;

public interface IJwtProvider
{
    string GetJwtToken(User user);

    Maybe<Guid> GetUserIdAsync(ClaimsPrincipal principal);
}