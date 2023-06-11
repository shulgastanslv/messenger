using Application.Common.Abstractions;
using Application.Common.Abstractions.Messaging;
using Domain.Entities.Users;
using Domain.Primitives.Result;

namespace Application.Users.Commands.UserRegistration;

public sealed class UserRegistrationCommandHandler : ICommandHandler<UserRegistrationCommand, Result<string>>
{
    private readonly IApplicationDbContext _applicationDbContext;

    private readonly IJwtProvider _jwtProvider;
    private readonly IUserRepository _userRepository;

    public UserRegistrationCommandHandler(IUserRepository userRepository, IApplicationDbContext applicationDbContext,
        IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _applicationDbContext = applicationDbContext;
        _jwtProvider = jwtProvider;
    }

    public async Task<Result<string>> Handle(UserRegistrationCommand request, CancellationToken cancellationToken)
    {
        var user = new User(
            Guid.NewGuid(),
            request.UserName,
            request.Password);

        var result = await _userRepository.CreateUserAsync(user, cancellationToken);

        if (result.IsFailure) return Result.Failure<string>(result.Error);

        await _applicationDbContext.SaveChangesAsync(cancellationToken);

        var token = _jwtProvider.GetJwtToken(user);

        return Result.Success(token);
    }
}