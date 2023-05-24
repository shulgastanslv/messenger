using Application.Common.Abstractions.Messaging;
using Application.Common.Interfaces;
using Domain.Primitives.Result;

namespace Application.Users.Queries.GetAllUsers;

public sealed class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery, UsersResponse>
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UsersResponse> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var result = await _userRepository.GetAllUsersAsync();

        return new UsersResponse(result);
    }
}