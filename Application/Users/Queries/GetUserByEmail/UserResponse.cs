using Domain.Entities.Users;

namespace Application.Users.Queries.GetUserByEmail;

public sealed record UserResponse(User user);