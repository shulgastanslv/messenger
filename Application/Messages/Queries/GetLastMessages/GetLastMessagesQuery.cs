using Application.Common.Abstractions.Messaging;
using Domain.Entities.Contacts;
using Microsoft.AspNetCore.Http;

namespace Application.Messages.Queries.GetLastMessages;

public sealed record GetLastMessagesQuery(Contact Sender, HttpContext Context) : IQuery<MessagesResponse>;