using Application.Common.Interfaces;

namespace Application.Users.Queries.GetUserById;

public sealed record GetUserByIdQuery(string Id) : IQuery<UserResponse>;