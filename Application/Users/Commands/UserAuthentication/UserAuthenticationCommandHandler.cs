using Application.Common.Abstractions;
using Application.Common.Abstractions.Messaging;
using Domain.Entities.Users;
using Domain.Primitives.Errors;
using Domain.Primitives.Result;

namespace Application.Users.Commands.UserAuthentication;

public class UserAuthenticationCommandHandler : ICommandHandler<UserAuthenticationCommand, Result<string>>
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IUserRepository _userRepository;

    public UserAuthenticationCommandHandler(IUserRepository userRepository, IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
    }

    public async Task<Result<string>> Handle(UserAuthenticationCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByUserNameAsync(request.UserName, cancellationToken);

        if (user.HasNoValue)
            return Result.Failure<string>(new Error("The user with the specified user name was not found."));

        if (user.Value!.Password != request.Password) return Result.Failure<string>(new Error("Password wasn't match"));

        var token = _jwtProvider.GetJwtToken(user.Value!);

        return Result.Success(token);
    }
}