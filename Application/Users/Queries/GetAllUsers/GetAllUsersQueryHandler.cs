using Application.Common.Abstractions.Messaging;
using Domain.Entities.Users;

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
        var users = await _userRepository.GetAllUsersAsync();

        return new UsersResponse(users);
    }
}