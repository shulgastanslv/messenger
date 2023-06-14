using Application.Common.Abstractions;
using Application.Common.Abstractions.Messaging;
using Domain.Entities.Chats;
using Domain.Entities.Messages;
using Domain.Primitives.Errors;
using Domain.Primitives.Result;
using Microsoft.Extensions.Logging;

namespace Application.Messages.Commands.SaveMessageCommand;

public class SaveMessageCommandHandler : ICommandHandler<SaveMessageCommand, Result>
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IMessageRepository _messageRepository;

    public SaveMessageCommandHandler(IMessageRepository messageRepository, IJwtProvider jwtProvider)
    {
        _messageRepository = messageRepository;
        _jwtProvider = jwtProvider;
    }

    public async Task<Result> Handle(SaveMessageCommand request, CancellationToken cancellationToken)
    {
        var senderId = await _jwtProvider.GetUserId(request.HttpContext.User);

        if (!senderId.HasValue) 
            return Result.Failure(new Error("Can't find sender identifier"));

        request.Message.Sender = senderId.Value;

        var result = await _messageRepository.SaveMessageAsync(
            request.Message,
            cancellationToken);

        return result;
    }
}