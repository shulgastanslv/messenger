using Application.Common.Abstractions.Messaging;
using Domain.Primitives.Result;

namespace Application.Users.Commands.UserAuthentication;

public record UserAuthenticationCommand(string UserName, string Password) : ICommand<Result<string>>;