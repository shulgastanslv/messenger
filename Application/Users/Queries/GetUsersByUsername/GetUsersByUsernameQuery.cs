using Application.Common.Abstractions.Messaging;
using Microsoft.AspNetCore.Http;

namespace Application.Users.Queries.GetUsersByUsername;

public sealed record GetUsersByUsernameQuery(
    string Username,
    HttpContext HttpContext) : IQuery<UsersResponse>;