using Domain.Entities.Users;

namespace Application.Users.Queries.GetAllUsers;

public sealed record UsersResponse(IEnumerable<User> Users);