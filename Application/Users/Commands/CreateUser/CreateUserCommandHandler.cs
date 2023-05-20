using Application.Common.Interfaces;
using Ardalis.GuardClauses;
using Domain.Entities;
using Domain.Shared;
using Microsoft.Extensions.Logging;

namespace Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
{
    private readonly IUserRepository _userRepository;

    private readonly ILogger<CreateUserCommand> _logger;

    private readonly IApplicationDbContext _applicationDbContext;

    public CreateUserCommandHandler(IUserRepository userRepository, IApplicationDbContext applicationDbContext, ILogger<CreateUserCommand> logger)
    {
        _userRepository = userRepository;
        _applicationDbContext = applicationDbContext;
        _logger = logger;
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

        await _userRepository.CreateUserAsync(user)!;

        await _applicationDbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation($"Executing MyCommand with data: {request}");

        return Result.Success();
    }
}