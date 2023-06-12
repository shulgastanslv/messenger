using Application.Common.Abstractions.Messaging;
using Domain.Entities.Users;
using Domain.Primitives.Result;

namespace Application.Users.Commands.UserUpdate;

public record UserUpdateCommand(User user) : ICommand<Result<string>>;