namespace Application.Users.Commands.UserRegistration;

public sealed record RegistrationResponse(string Token, Guid Id);