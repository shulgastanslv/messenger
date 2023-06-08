using System.Security.Claims;
using Application.Common.Abstractions;
using Application.Common.Abstractions.Messaging;
using Domain.Entities.Chats;
using Domain.Entities.Messages;
using Domain.Entities.Users;
using Domain.Primitives.Result;

namespace Application.Messages.Commands.SaveMessageCommand;

public class SaveMessageCommandHandler : ICommandHandler<SaveMessageCommand, Result>
{
    private readonly IMessageRepository _messageRepository;

    private readonly IChatRepository _chatRepository;

    private readonly IJwtProvider _jwtProvider;

    public SaveMessageCommandHandler(IMessageRepository messageRepository, IChatRepository chatRepository, IJwtProvider jwtProvider)
    {
        _messageRepository = messageRepository; 
        _chatRepository = chatRepository;
        _jwtProvider = jwtProvider;
    }
    public async Task<Result> Handle(SaveMessageCommand request, CancellationToken cancellationToken)
    {
        var maybeSenderId = _jwtProvider.GetUserIdAsync(request.Context.User);
        if (maybeSenderId.HasNoValue)
        {
            return Result.Failure(new("Can't find sender identifier"));
        }

        var chatResult = await _chatRepository.GetChatAsync(maybeSenderId.Value,
            request.Message.Receiver, cancellationToken);

        if (chatResult.IsFailure)
        {
            return Result.Failure(chatResult.Error);
        }

        var result = await _messageRepository.SaveMessageAsync(
            chatResult.Value,
            request.Message,
            cancellationToken);

        return result;
    }
}
