using Application.Common.Abstractions.Messaging;
using Domain.Entities.Contacts;
using Microsoft.AspNetCore.Http;

namespace Application.Messages.Queries.GetLastMessagesAsync;

public sealed record GetLastMessagesQuery(Contact Sender, HttpContext Context) : IQuery<MessagesResponse>;
