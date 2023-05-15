using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Shared;

namespace Application.Users.Queries.FindUser;

public class FindUserQueryHandler : IQueryHandler<FindUserQuery, User>
{
    private readonly IUserRepository _userRepository;

    public FindUserQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<Result<User>> Handle(FindUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindUserAsync(request.email, request.password)!;

        return Result<User>.Success();
    }
}