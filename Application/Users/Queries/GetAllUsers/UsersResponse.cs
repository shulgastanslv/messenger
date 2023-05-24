using Domain.Entities.User;

namespace Application.Users.Queries.GetAllUsers;

public sealed record UsersResponse(IEnumerable<User> Users);