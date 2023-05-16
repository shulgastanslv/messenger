using Application.Common.Interfaces;
using Domain.Shared;

namespace Application.Users.Commands.AuthenticateUser;

public class AuthenticateUserCommandHandler : ICommandHandler<AuthenticateUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IApplicationDbContext _applicationDbContext;

    public AuthenticateUserCommandHandler(IUserRepository userRepository, IApplicationDbContext applicationDbContext)
    {
        _userRepository = userRepository;
        _applicationDbContext = applicationDbContext;
    }
    public async Task<Result> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
    {
        var user = _userRepository.AuthenticateUserAsync(request.email, request.password);

        return user ? Result.Success() : Result.Failure(new []{"User not found!"});
    }
}