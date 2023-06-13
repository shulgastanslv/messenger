using Application.Common.Abstractions;
using Application.Common.Abstractions.Messaging;
using Domain.Entities.Contacts;
using Domain.Entities.Users;
using Domain.Primitives.Errors;
using Domain.Primitives.Result;

namespace Application.Users.Queries.GetUsersByUsername;

public class GetUsersByUsernameQueryHandler : IQueryHandler<GetUsersByUsernameQuery, UsersResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;

    public GetUsersByUsernameQueryHandler(IUserRepository userRepository, IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
    }
    public async Task<UsersResponse> Handle(GetUsersByUsernameQuery request, CancellationToken cancellationToken)
    {
        var senderId = await _jwtProvider.GetUserId(request.HttpContext.User);

        if (senderId.HasValue)
            return new UsersResponse(
                Result.Failure<IEnumerable<Contact>>(new Error("Used don't recognized")));

        var users = await _userRepository.GetUsersByUsernameAsync(request.Username, cancellationToken);

        var contacts = users.Select(u =>
            new Contact(
                u.Id,
                u.Username,
                u.SentChats?.Find(
                        c => c.ReceiverId == senderId!.Value)?
                    .ChatId));

        return new UsersResponse(Result.Success(contacts));
    }
}