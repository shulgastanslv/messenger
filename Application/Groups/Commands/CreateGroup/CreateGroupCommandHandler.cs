using Application.Common.Abstractions;
using Application.Common.Abstractions.Messaging;
using Domain.Entities.Groups;
using Domain.Entities.Messages;
using Domain.Entities.UserGroups;
using Domain.Primitives.Errors;
using Domain.Primitives.Result;

namespace Application.Groups.Commands.CreateGroup;

public class CreateGroupCommandHandler : ICommandHandler<CreateGroupCommand, Result<Group>>
{
    private readonly IGroupRepository _groupRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IMessageRepository _messageRepository;
    private readonly IUserGroupRepository _userGroupRepository;

    public CreateGroupCommandHandler(IGroupRepository groupRepository,
        IUserGroupRepository userGroupRepository, IJwtProvider jwtProvider, IMessageRepository messageRepository)
    {
        _groupRepository = groupRepository;
        _userGroupRepository = userGroupRepository;
        _jwtProvider = jwtProvider;
        _messageRepository = messageRepository;
    }

    public async Task<Result<Group>> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        var maybeUserId = await _jwtProvider.GetUserIdAsync(request.HttpContext.User);
        if (!maybeUserId.HasValue)
            return Result.Failure<Group>(
                new Error("Can't find sender identifier"));

        var group = new Group(
            Guid.NewGuid(),
            request.Name + " • Group");

        var groupResult = await _groupRepository.CreateGroupAsync(group, cancellationToken);

        if (groupResult.IsFailure)
            return Result.Failure<Group>(groupResult.Error);

        var userGroup = new UserGroup(
            maybeUserId.Value,
            group.Id);

        var userGroupResult = await _userGroupRepository.AddUserGroupAsync(userGroup, cancellationToken);


        if (userGroupResult.IsFailure)
            Result.Failure<Group>(userGroupResult.Error);

        var message = new Message(
            Guid.NewGuid(),
            $"Created new group {request.Name}",
            Guid.Empty,
            group.Id);

        var result = await _messageRepository.SaveMessageAsync(
            message,
            cancellationToken);


        if (result.IsFailure)
            Result.Failure<Group>(result.Error);

        return groupResult;
    }
}