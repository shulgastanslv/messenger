using Application.Common.Abstractions;
using Application.Common.Abstractions.Messaging;
using Domain.Primitives.Result;

namespace Application.Users.Commands.UserEmailVerification;

public class UserEmailVerificationCommandHandler : ICommandHandler<UserEmailVerificationCommand, Result>
{
    private readonly IEmailService _emailService;
    public UserEmailVerificationCommandHandler(IEmailService emailService)
    {
        _emailService = emailService;
    }
    public async Task<Result> Handle(UserEmailVerificationCommand request, CancellationToken cancellationToken)
    {
        await _emailService.SendEmailAsync(request.recipient, request.body);

        return Result.Success();
    }
}