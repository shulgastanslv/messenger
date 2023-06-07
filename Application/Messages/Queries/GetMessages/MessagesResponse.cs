using Domain.Entities.Messages;

namespace Application.Messages.Queries.GetMessages;

public sealed record MessagesResponse(List<Message> Messages);
