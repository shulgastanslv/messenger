using Application.Common.Abstractions.Messaging;

namespace Application.Users.Queries.GetAllUsers;

public sealed record GetAllUsersQuery : IQuery<UsersResponse>;