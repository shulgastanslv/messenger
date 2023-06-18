using Application.Common.Abstractions.Messaging;
using Domain.Entities.Groups;
using Domain.Primitives.Result;
using Microsoft.AspNetCore.Http;

namespace Application.Groups.Commands.CreateGroup;

public sealed record CreateGroupCommand(
    string Name,
    HttpContext HttpContext) : ICommand<Result<Group>>;