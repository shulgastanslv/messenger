using Domain.Entities;

namespace Application.Users.Queries.GetAllUsers;

public sealed record UsersResponse(List<User> Users);