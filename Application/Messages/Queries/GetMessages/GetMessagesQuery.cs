using Application.Common.Abstractions.Messaging;

namespace Application.Messages.Queries.GetMessages;

public sealed record GetMessagesQuery(Guid receiver, Guid sender) : IQuery<MessagesResponse>;
