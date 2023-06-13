using Application.Common.Abstractions;
using Application.Common.Abstractions.Messaging;
using Domain.Entities.Users;
using Domain.Primitives.Result;

namespace Application.Users.Commands.UserRegistration;

public sealed class UserRegistrationCommandHandler : ICommandHandler<UserRegistrationCommand, Result<RegistrationResponse>>
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

    public async Task<Result<RegistrationResponse>> Handle(UserRegistrationCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.NewGuid();

        var user = new User(id, request.Username, request.Password);

        var result = await _userRepository.CreateAsync(user, cancellationToken);

        if (result.IsFailure) 
            return Result.Failure<RegistrationResponse>(result.Error);

        await _applicationDbContext.SaveChangesAsync(cancellationToken);

        var token = _jwtProvider.GetJwtToken(user);

        return Result.Success(new RegistrationResponse(token, id));
    }
}