using Application.Common.Abstractions.Messaging;
using Domain.Primitives.Result;

namespace Application.Users.Commands.UserAuthentication;

public record UserAuthenticationCommand(string Email, string Password) : ICommand<Result<string>>;