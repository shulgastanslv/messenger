using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Shared;
using MediatR;

namespace Application.Users.Queries.GetAllUsers;

public sealed class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery, UsersResponse>
{
    private readonly IApplicationDbContext _context;

    private readonly IUserRepository _userRepository;

    public GetAllUsersQueryHandler(IApplicationDbContext context, IUserRepository userRepository)
    {
        _context = context;
        _userRepository = userRepository;
    }

    public async Task<Result<UsersResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var result = await _userRepository.GetAllUsersAsync();

        var response = new UsersResponse(result.ToList());

        return Result<UsersResponse>.Success(response);
    }
}