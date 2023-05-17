using Application.Common.Interfaces;
using Ardalis.GuardClauses;
using Domain.Entities;
using Domain.Shared;

namespace Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
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
            Id = request.Id,
            UserName = request.UserName,
            Email = request.Email,
            Password = request.Password,
            CreationTime = request.CreationTime
        };

        await _userRepository.CreateUserAsync(user)!;

        await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}