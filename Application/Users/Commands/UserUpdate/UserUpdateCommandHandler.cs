using Application.Common.Abstractions;
using Application.Common.Abstractions.Messaging;
using Domain.Entities.Users;
using Domain.Primitives.Result;

namespace Application.Users.Commands.UserUpdate;

public class UserUpdateCommandHandler : ICommandHandler<UserUpdateCommand, Result<string>>
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IUserRepository _userRepository;

    public UserUpdateCommandHandler(IJwtProvider jwtProvider, IUserRepository userRepository)
    {
        _jwtProvider = jwtProvider;
        _userRepository = userRepository;
    }

    public async Task<Result<string>> Handle(UserUpdateCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.UpdateUserAsync(request.user, cancellationToken);

        var token = _jwtProvider.GetJwtToken(user.Value!);

        return Result.Success(token);
    }
}