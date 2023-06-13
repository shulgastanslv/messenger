using Application.Common.Abstractions.Messaging;
using Application.Common.Abstractions;
using Domain.Entities.Chats;
using Domain.Primitives.Errors;
using Domain.Primitives.Result;

namespace Application.Chats.Commands.CreateChat;

public sealed class CreateChatCommandHandler : ICommandHandler<CreateChatCommand, Result<Chat>>
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IChatRepository _chatRepository;

    public CreateChatCommandHandler(IJwtProvider jwtProvider, IChatRepository chatRepository)
    {
        _jwtProvider = jwtProvider;
        _chatRepository = chatRepository;
    }

    public async Task<Result<Chat>> Handle(CreateChatCommand request, CancellationToken cancellationToken)
    {
        var maybeUserId = await _jwtProvider.GetUserId(request.HttpContext.User);

        if (!maybeUserId.HasValue) 
            return Result.Failure<Chat>(new Error("Can't find sender identifier"));

        var maybeChat = await _chatRepository.CreateChatAsync(
            maybeUserId.Value, request.Contact.Id, cancellationToken);

        return maybeChat;
    }
}