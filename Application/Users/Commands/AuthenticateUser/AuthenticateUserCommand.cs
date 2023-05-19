using Application.Common.Interfaces;
using Domain.Entities;

namespace Application.Users.Commands.AuthenticateUser;

public record AuthenticateUserCommand(string Email, string Password) : ICommand<bool>;