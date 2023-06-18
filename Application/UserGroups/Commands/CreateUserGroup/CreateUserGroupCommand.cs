using Application.Common.Abstractions.Messaging;
using Domain.Primitives.Result;
using Microsoft.AspNetCore.Http;

namespace Application.UserGroups.Commands.CreateUserGroup;

public sealed record CreateUserGroupCommand(
    Guid GroupId,
    HttpContext HttpContext) : ICommand<Result<Guid>>;