using Application.Common.Abstractions.Messaging;

namespace Application.Users.Queries.GetUserByUserName;

public sealed record GetUserByUserNameQuery(string UserName) : IQuery<UserResponse>;