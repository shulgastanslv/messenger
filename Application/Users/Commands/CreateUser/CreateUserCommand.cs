using Application.Common.Interfaces;

namespace Application.Users.Commands.CreateUser;

public sealed record CreateUserCommand(
    Guid id,
    string UserName, 
    string Email, 
    string Password, 
    DateTime CreationTime) : ICommand;