using Application.Common.Abstractions;
using Application.Common.Abstractions.Messaging;
using Domain.Entities.User;
using Domain.Primitives.Result;

namespace Application.Users.Commands.AuthenticateUser;

public class AuthenticateUserCommandHandler : ICommandHandler<AuthenticateUserCommand, Result<string>>
{
    private readonly IUserRepository _userRepository;

    private readonly IJwtProvider _jwtProvider;

    public AuthenticateUserCommandHandler(IUserRepository userRepository, IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
    }

    public async Task<Result<string>> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByEmail(request.Email, cancellationToken);

        string token = _jwtProvider.GetJwtToken(user.Value!);

        return Result.Success(token);
    }
}