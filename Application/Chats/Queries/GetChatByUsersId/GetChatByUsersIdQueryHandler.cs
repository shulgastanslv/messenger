using Application.Common.Abstractions;
using Application.Common.Abstractions.Messaging;
using Domain.Entities.Chats;
using Domain.Primitives.Errors;
using Domain.Primitives.Result;

namespace Application.Chats.Queries.GetChatByUsersId;

public class GetChatByUsersIdQueryHandler : IQueryHandler<GetChatByUsersIdQuery, Result<ChatResponse>>
{
    private readonly IChatRepository _chatRepository;
    private readonly IJwtProvider _jwtProvider;

    public GetChatByUsersIdQueryHandler(IChatRepository chatRepository, IJwtProvider jwtProvider)
    {
        _chatRepository = chatRepository;
        _jwtProvider = jwtProvider;
    }

    public async Task<Result<ChatResponse>> Handle(GetChatByUsersIdQuery request, CancellationToken cancellationToken)
    {
        var maybeUserId = await _jwtProvider.GetUserIdAsync(request.HttpContext.User);

        if (!maybeUserId.HasValue)
            return Result.Failure<ChatResponse>(new Error("Can't find sender identifier"));

        var maybeChat = await _chatRepository.GetChatByUsersIdAsync(
            maybeUserId.Value, request.Contact.Id, cancellationToken);

        if (maybeChat == null)
            return Result.Failure<ChatResponse>(new Error("Can't find chat"));

        return Result.Success(new ChatResponse(maybeChat));
    }
}