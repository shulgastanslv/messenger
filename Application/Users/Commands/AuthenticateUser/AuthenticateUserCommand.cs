using Application.Common.Interfaces;

namespace Application.Users.Commands.AuthenticateUser;

public record AuthenticateUserCommand(string email, string password) : ICommand;