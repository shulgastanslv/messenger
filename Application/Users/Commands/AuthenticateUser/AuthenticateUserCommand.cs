using Application.Common.Interfaces;

namespace Application.Users.Commands.AuthenticateUser;

public record AuthenticateUserCommand(string Email, 
    string Password) : ICommand<bool>;