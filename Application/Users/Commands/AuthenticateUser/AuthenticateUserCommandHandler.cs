using Application.Common.Abstractions.Messaging;
using Application.Common.Interfaces;
using Domain.Primitives.Result;

namespace Application.Users.Commands.AuthenticateUser;

public class AuthenticateUserCommandHandler : ICommandHandler<AuthenticateUserCommand, Result>
{
    private readonly IUserRepository _userRepository;

    public AuthenticateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.AuthenticateUserAsync(request.Email, 
            request.Password, cancellationToken);

        return user.HasValue ? Result.Success() : 
            Result.Failure(new("The user with the specified user name was not found."));
    }
}