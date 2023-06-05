using Application.Common.Abstractions.Messaging;
using Domain.Primitives.Result;

namespace Application.Users.Commands.UserEmailVerification;

public record UserEmailVerificationCommand(string recipient, string body) : ICommand<Result>;