namespace Application.Users.Commands.UserAuthentication;

public sealed record AuthenticationResponse(string Token, Guid Id);