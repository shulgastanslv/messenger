using Domain.Entities.Users;

namespace Application.Common.Abstractions;

public interface IJwtProvider
{
    string GetJwtToken(User user);
}