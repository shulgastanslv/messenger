using Application.Common.Abstractions;
using Application.Common.Abstractions.Messaging;
using Domain.Entities.Contacts;
using Domain.Entities.Groups;
using Domain.Entities.Users;
using Domain.Primitives.Errors;
using Domain.Primitives.Result;

namespace Application.Users.Queries.GetUsersByUsername;

public class GetUsersByUsernameQueryHandler : IQueryHandler<GetUsersByUsernameQuery, UsersResponse>
{
    private readonly IGroupRepository _groupRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IUserRepository _userRepository;

    public GetUsersByUsernameQueryHandler(IUserRepository userRepository, IJwtProvider jwtProvider,
        IGroupRepository groupRepository)
    {
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
        _groupRepository = groupRepository;
    }

    public async Task<UsersResponse> Handle(GetUsersByUsernameQuery request, CancellationToken cancellationToken)
    {
        var senderId = await _jwtProvider.GetUserIdAsync(request.HttpContext.User);

        if (!senderId.HasValue)
            return new UsersResponse(
                Result.Failure<IEnumerable<Contact>>(new Error("Used don't recognized")));

        var users = await _userRepository.GetUsersByUsernameAsync(request.Username, cancellationToken);


        var contacts = users.Select(u =>
                new Contact(
                    u.Id,
                    u.Username,
                    u.SentChats?.Find(
                            c => c.ReceiverId == senderId.Value)?
                        .ChatId))
            .ToList();

        var groups = await _groupRepository.GetGroupsByUsernameAsync(request.Username, cancellationToken);

        var groupContacts = groups.Select(g =>
                {
                    var isGroupId = g.UserGroups?.Any(ug =>
                        ug.UserId.Equals(senderId.Value)) ?? false;

                    var groupId = isGroupId ? g.Id : Guid.Empty;

                    return new Contact(
                        groupId,
                        g.Name,
                        g.Id);
                }
            )
            .ToList();

        contacts.AddRange(groupContacts);

        return new UsersResponse(Result.Success<IEnumerable<Contact>>(contacts));
    }
}