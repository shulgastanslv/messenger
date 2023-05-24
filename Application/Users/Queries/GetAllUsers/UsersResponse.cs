using Domain.Entities;

namespace Application.Users.Queries.GetAllUsers;

public sealed record UsersResponse(IEnumerable<User> Users);