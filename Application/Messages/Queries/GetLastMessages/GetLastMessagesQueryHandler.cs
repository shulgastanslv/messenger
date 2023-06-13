using Application.Common.Abstractions;
using Application.Common.Abstractions.Messaging;
using Domain.Entities.Messages;

namespace Application.Messages.Queries.GetLastMessages;

public class GetLastMessagesQueryHandler : IQueryHandler<GetLastMessagesQuery, MessagesResponse>
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IMessageRepository _messageRepository;

    public GetLastMessagesQueryHandler(IMessageRepository messageRepository, IJwtProvider jwtProvider)
    {
        _messageRepository = messageRepository;
        _jwtProvider = jwtProvider;
    }

    public async Task<MessagesResponse> Handle(GetLastMessagesQuery request, CancellationToken cancellationToken)
    {
        return null;
    }
}