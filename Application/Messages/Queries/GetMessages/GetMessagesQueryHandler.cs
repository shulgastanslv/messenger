using Application.Common.Abstractions.Messaging;
using Domain.Entities.Messages;

namespace Application.Messages.Queries.GetMessages;

public class GetMessagesQueryHandler : IQueryHandler<GetMessagesQuery, MessagesResponse>
{
    private readonly IMessageRepository _messageRepository;
    public GetMessagesQueryHandler(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }
    public async Task<MessagesResponse> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
    {
        var result = await _messageRepository.GetMessagesAsync(request.receiver, request.sender, cancellationToken);

        return new MessagesResponse(result.Value);
    }
}