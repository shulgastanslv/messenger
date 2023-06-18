using Application.Common.Abstractions;
using Application.Common.Abstractions.Messaging;
using Domain.Entities.UserGroups;
using Domain.Primitives.Errors;
using Domain.Primitives.Result;

namespace Application.UserGroups.Commands.CreateUserGroup;

public class CreateUserGroupCommandHandler : ICommandHandler<CreateUserGroupCommand, Result<Guid>>
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IUserGroupRepository _userGroupRepository;

    public CreateUserGroupCommandHandler(IUserGroupRepository userGroupRepository, IJwtProvider jwtProvider)
    {
        _userGroupRepository = userGroupRepository;
        _jwtProvider = jwtProvider;
    }


    public async Task<Result<Guid>> Handle(CreateUserGroupCommand request, CancellationToken cancellationToken)
    {
        var maybeUserId = await _jwtProvider.GetUserIdAsync(request.HttpContext.User);

        if (!maybeUserId.HasValue)
            return Result.Failure<Guid>(
                new Error("Can't find sender identifier"));

        var userGroup = new UserGroup(
            maybeUserId.Value,
            request.GroupId);

        var userGroupResult = await _userGroupRepository.AddUserGroupAsync(userGroup, cancellationToken);

        if (userGroupResult.IsFailure)
            Result.Failure<UserGroup>(userGroupResult.Error);

        return Result.Success(userGroupResult.Value.GroupId);
    }
}