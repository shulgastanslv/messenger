using Application.Common.Abstractions.Messaging;
using Domain.Entities.Contacts;
using Microsoft.AspNetCore.Http;

namespace Application.Messages.Queries.GetLastMessages;

public sealed record class GetLastMessagesQuery(
    DateTime LastResponseTime,
    HttpContext HttpContext) : IQuery<LastMessagesResponse>;