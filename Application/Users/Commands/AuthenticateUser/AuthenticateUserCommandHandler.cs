using Application.Common.Interfaces;
using Ardalis.GuardClauses;
using Domain.Shared;
using Microsoft.Extensions.Logging;

namespace Application.Users.Commands.AuthenticateUser;

public class AuthenticateUserCommandHandler : ICommandHandler<AuthenticateUserCommand, bool>
{
    private readonly IUserRepository _userRepository;

    public AuthenticateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<Result<bool>> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.AuthenticateUserAsync(request.Email, request.Password);

        return user ? Result<bool>.Success(user) : Result<bool>.Failure(new []{""});
    }
}