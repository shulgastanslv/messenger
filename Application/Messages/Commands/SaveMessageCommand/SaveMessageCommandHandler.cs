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
    private readonly IChatRepository _chatRepository;

    private readonly IJwtProvider _jwtProvider;

    private readonly ILogger<SaveMessageCommandHandler> _logger;
    private readonly IMessageRepository _messageRepository;

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
        return null;
    }
}