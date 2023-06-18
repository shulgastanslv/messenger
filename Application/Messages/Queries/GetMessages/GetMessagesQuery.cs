using Application.Common.Abstractions.Messaging;
using Domain.Entities.Contacts;

namespace Application.Messages.Queries.GetMessages;

public sealed record GetMessagesQuery(
    Contact Sender) : IQuery<MessagesResponse>;