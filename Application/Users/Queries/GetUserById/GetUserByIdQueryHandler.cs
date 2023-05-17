using Application.Common.Interfaces;
using Application.Users.Queries.GetAllUsers;
using Domain.Shared;

namespace Application.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserResponse>
{
    private readonly IUserRepository _userRepository;

    private readonly IApplicationDbContext _applicationDbContext;

    public GetUserByIdQueryHandler(IUserRepository userRepository, IApplicationDbContext applicationDbContext)
    {
        _userRepository = userRepository;
        _applicationDbContext = applicationDbContext;
    }

    public async Task<Result<UserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _userRepository.GetUserByIdAsync(request.Id);

        var response = new UserResponse(result);

        return Result<UserResponse>.Success(response);
    }
}