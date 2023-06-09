using Domain.Entities.Messages;
using Domain.Primitives.Result;

namespace Application.Messages.Queries.GetLastMessagesAsync;

public sealed record MessagesResponse(Result<IEnumerable<Message>> Messages);