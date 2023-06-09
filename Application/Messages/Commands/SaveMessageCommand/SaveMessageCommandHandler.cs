using System.Security.Claims;
using Application.Common.Abstractions;
using Application.Common.Abstractions.Messaging;
using Domain.Entities.Chats;
using Domain.Entities.Messages;
using Domain.Entities.Users;
using Domain.Primitives.Result;
using Microsoft.Extensions.Logging;

namespace Application.Messages.Commands.SaveMessageCommand;

public class SaveMessageCommandHandler : ICommandHandler<SaveMessageCommand, Result>
{
    private readonly IMessageRepository _messageRepository;

    private readonly IChatRepository _chatRepository;

    private readonly IJwtProvider _jwtProvider;

    private readonly ILogger<SaveMessageCommandHandler> _logger;

    public SaveMessageCommandHandler(IMessageRepository messageRepository, IChatRepository chatRepository, 
        IJwtProvider jwtProvider, ILogger<SaveMessageCommandHandler> logger)
    {
        _messageRepository = messageRepository; 
        _chatRepository = chatRepository;
        _jwtProvider = jwtProvider;
        _logger = logger;
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

        if (chatResult.HasNoValue)
        {
            _logger.LogInformation("Chat with the user {user1} and {user2} not found",
                maybeSenderId.Value, request.Message.Receiver);

            chatResult = (await _chatRepository.CreateChatAsync(maybeSenderId.Value, request.Message.Receiver,
                cancellationToken)).Value;
        }

        var result = await _messageRepository.SaveMessageAsync(
            chatResult.Value,
            request.Message,
            cancellationToken);

        _logger.LogInformation("Message saved for chat {chatId}", chatResult.Value.Id);

        return result;
    }
}
