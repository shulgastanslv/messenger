using Application.Common.Abstractions.Messaging;
using Domain.Entities.Users;
using Domain.Primitives.Errors;
using Domain.Primitives.Result;

namespace Application.Users.Queries.GetUserByEmail;

public class GetUserByEmailQueryHandler : IQueryHandler<GetUserByEmailQuery, UserResponse>
{
    private readonly IUserRepository _userRepository;

    public GetUserByEmailQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserResponse> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByEmailAsync(request.email, cancellationToken);

        return new UserResponse(user.Value!);
    }
}