using Application.Common.Abstractions.Messaging;
using Domain.Entities.Contacts;
using Microsoft.AspNetCore.Http;

namespace Application.Messages.Queries.GetMessages;

public sealed record GetMessagesQuery(Contact Sender, HttpContext Context) : IQuery<MessagesResponse>;
