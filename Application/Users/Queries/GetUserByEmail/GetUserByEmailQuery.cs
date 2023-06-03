using Application.Common.Abstractions.Messaging;

namespace Application.Users.Queries.GetUserByEmail;

public sealed record GetUserByEmailQuery(string email) : IQuery<UserResponse>;
