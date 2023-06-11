using Application.Common.Abstractions;
using Application.Common.Abstractions.Messaging;
using Domain.Entities.Messages;

namespace Application.Messages.Queries.GetMessages;

public class GetMessagesQueryHandler : IQueryHandler<GetMessagesQuery, MessagesResponse>
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IMessageRepository _messageRepository;

    public GetMessagesQueryHandler(IMessageRepository messageRepository, IJwtProvider jwtProvider)
    {
        _messageRepository = messageRepository;
        _jwtProvider = jwtProvider;
    }

    public async Task<MessagesResponse> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
    {
        var maybeSenderId = _jwtProvider.GetUserIdAsync(request.Context.User);

        return new MessagesResponse(await _messageRepository.GetMessagesAsync(maybeSenderId.Value,
            request.Sender.Id, cancellationToken));
    }
}