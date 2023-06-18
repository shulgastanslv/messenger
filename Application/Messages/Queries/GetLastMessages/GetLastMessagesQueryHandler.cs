using Application.Common.Abstractions;
using Application.Common.Abstractions.Messaging;
using Domain.Entities.Messages;
using Domain.Entities.Users;
using Domain.Primitives.Errors;
using Domain.Primitives.Result;

namespace Application.Messages.Queries.GetLastMessages;

public class GetLastMessagesQueryHandler : IQueryHandler<GetLastMessagesQuery, LastMessagesResponse>
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IMessageRepository _messageRepository;
    private readonly IUserRepository _userRepository;

    public GetLastMessagesQueryHandler(IJwtProvider jwtProvider,
        IUserRepository userRepository, IMessageRepository messageRepository)
    {
        _jwtProvider = jwtProvider;
        _userRepository = userRepository;
        _messageRepository = messageRepository;
    }

    public async Task<LastMessagesResponse> Handle(GetLastMessagesQuery request, CancellationToken cancellationToken)
    {
        var userId = await _jwtProvider.GetUserIdAsync(request.HttpContext.User);

        if (!userId.HasValue)
            return new LastMessagesResponse(Result.Failure<IEnumerable<Message>>
                (new Error("User don't recognized")));

        var user = await _userRepository.GetByIdAsync(userId.Value, cancellationToken);

        if (user == null)
            return new LastMessagesResponse(Result.Failure<IEnumerable<Message>>
                (new Error("User doesn't exist")));


        var receivedChats = user.ReceivedChats;

        var messages = new List<Message>();

        if (receivedChats == null)
            return new LastMessagesResponse(Result.Success<IEnumerable<Message>>(messages));

        foreach (var receivedChat in receivedChats)
        {
            var lastMessages = await _messageRepository.GetLastMessagesAsync(
                receivedChat.ChatId, request.LastResponseTime, cancellationToken);

            if (lastMessages == null)
                continue;

            messages.AddRange(lastMessages);
        }

        return new LastMessagesResponse(Result.Success<IEnumerable<Message>>(messages));
    }
}