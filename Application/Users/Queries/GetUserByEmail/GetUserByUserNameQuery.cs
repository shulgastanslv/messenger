using Application.Common.Abstractions.Messaging;

namespace Application.Users.Queries.GetUserByEmail;

public sealed record GetUserByUserNameQuery(string email) : IQuery<UserResponse>;