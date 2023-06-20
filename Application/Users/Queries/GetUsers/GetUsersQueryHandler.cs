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
        var senderId = await _jwtProvider.GetUserIdAsync(request.HttpContext.User);

        if (!senderId.HasValue)
            return new UsersResponse(
                Result.Failure<IEnumerable<Contact>?>(new Error("Used don't recognized")));

        var sender = await _userRepository.GetByIdAsync(senderId.Value, cancellationToken);

        if (sender == null)
            return new UsersResponse(
                Result.Failure<IEnumerable<Contact>?>(new Error("Used doesn't exist")));


        var contacts = sender.SentChats
            ?.Select(c =>
            {
                if (c.Receiver != null)
                    return new Contact(
                        c.Receiver.Id,
                        c.Receiver.Username,
                        c.ChatId);
                return null;
            })
            .ToList();

        var groupContacts = sender.UserGroups
            ?.Select(ug =>
            {
                if (ug.Group != null)
                    return new Contact(
                        ug.GroupId,
                        ug.Group.Name,
                        ug.GroupId);
                return null;
            })
            .ToList();

        if (groupContacts != null) contacts?.AddRange(groupContacts);

        return new UsersResponse(Result.Success<IEnumerable<Contact>?>(contacts));
    }
}