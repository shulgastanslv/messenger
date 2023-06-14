using Application.Common.Abstractions;
using Application.Common.Abstractions.Messaging;
using Domain.Entities.Contacts;
using Domain.Entities.Users;
using Domain.Primitives.Errors;
using Domain.Primitives.Result;

namespace Application.Users.Queries.GetUsers;

public sealed class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, UsersResponse>
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IUserRepository _userRepository;

    public GetUsersQueryHandler(IUserRepository userRepository, IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
    }

    public async Task<UsersResponse> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var senderId = await _jwtProvider.GetUserId(request.HttpContext.User);

        if (!senderId.HasValue)
            return new UsersResponse(
                Result.Failure<IEnumerable<Contact>?>(new Error("Used don't recognized")));

        var sender = await _userRepository.GetByIdAsync(senderId.Value, cancellationToken);

        if (sender == null)
            return new UsersResponse(
                Result.Failure<IEnumerable<Contact>?>(new Error("Used doesn't exist")));


        var contacts = sender.SentChats?.Select(c => new Contact(c.Receiver.Id, c.Receiver.Username, c.ChatId))
            .ToList();

        return new UsersResponse(Result.Success<IEnumerable<Contact>?>(contacts));
    }
}