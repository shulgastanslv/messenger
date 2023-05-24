using Application.Common.Abstractions.Messaging;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Primitives.Result;

namespace Application.Users.Commands.CreateUser;

public sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Result>
{
    private readonly IUserRepository _userRepository;

    private readonly IApplicationDbContext _applicationDbContext;

    public CreateUserCommandHandler(IUserRepository userRepository, IApplicationDbContext applicationDbContext)
    {
        _userRepository = userRepository;
        _applicationDbContext = applicationDbContext;
    }

    public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        { 
            Id = Guid.NewGuid(),
            UserName = request.UserName,
            Email = request.Email,
            Password = request.Password,
            CreationTime = request.CreationTime
        };

        var result = await _userRepository.CreateUserAsync(user, cancellationToken);

        if (result.IsFailure)
        {
            return result;
        }

        await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}