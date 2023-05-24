using Domain.Entities.User;

namespace Application.Common.Abstractions;

public interface IJwtProvider
{
    string GetJwtToken(User user);
}