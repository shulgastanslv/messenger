using Application.Common.Abstractions.Messaging;
using Domain.Primitives.Result;

namespace Application.Users.Commands.UserAuthentication;

public record UserAuthenticationCommand(string Username, string Password) : ICommand<Result<AuthenticationResponse>>;