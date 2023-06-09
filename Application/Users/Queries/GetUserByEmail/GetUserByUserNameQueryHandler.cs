using Application.Common.Abstractions.Messaging;
using Domain.Entities.Users;
using Domain.Primitives.Errors;
using Domain.Primitives.Result;

namespace Application.Users.Queries.GetUserByEmail;

public class GetUserByUserNameQueryHandler : IQueryHandler<GetUserByUserNameQuery, UserResponse>
{
    private readonly IUserRepository _userRepository;

    public GetUserByUserNameQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserResponse> Handle(GetUserByUserNameQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByUserNameAsync(request.email, cancellationToken);

        return new UserResponse(user.Value!);
    }
}