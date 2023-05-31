using Application.Common.Abstractions.Messaging;
using Domain.Primitives.Result;

namespace Application.Users.Commands.UserRegistration;

public sealed record UserRegistrationCommand(
    Guid Id,
    string UserName, 
    string Email, 
    string Password, 
    DateTime CreationTime) : ICommand<Result<string>>;