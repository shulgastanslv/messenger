using Application.Common.Abstractions.Messaging;
using Microsoft.AspNetCore.Http;

namespace Application.Users.Queries.GetUsers;

public sealed record GetUsersQuery(
    HttpContext HttpContext) : IQuery<UsersResponse>;