using Application.Common.Abstractions;
using Application.Common.Abstractions.Messaging;
using Domain.Entities.Users;
using Domain.Primitives.Errors;
using Domain.Primitives.Result;

namespace Application.Users.Commands.UserAuthentication;

public class UserAuthenticationCommandHandler : ICommandHandler<UserAuthenticationCommand, Result<AuthenticationResponse>>
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IUserRepository _userRepository;

    public UserAuthenticationCommandHandler(IUserRepository userRepository, IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
    }

    public async Task<Result<AuthenticationResponse>> Handle(UserAuthenticationCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByUsernameAsync(request.Username, cancellationToken)!;

        if (user == null)
            return Result.Failure<AuthenticationResponse>(
                new Error("The user with the specified user name was not found."));

        if (user.Password != request.Password)
            return Result.Failure<AuthenticationResponse>
                (new Error("Password wasn't match"));

        var token = _jwtProvider.GetJwtToken(user);

        return Result.Success(new AuthenticationResponse(token, user.Id));
    }
}