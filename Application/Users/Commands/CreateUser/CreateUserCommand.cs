using Application.Common.Abstractions.Messaging;
using Domain.Primitives.Result;

namespace Application.Users.Commands.CreateUser;

public sealed record CreateUserCommand(
    Guid Id,
    string UserName, 
    string Email, 
    string Password, 
    DateTime CreationTime) : ICommand<Result>;