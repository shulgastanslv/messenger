using Application.Common.Abstractions.Messaging;
using Domain.Entities.Contacts;
using Domain.Primitives.Result;
using Microsoft.AspNetCore.Http;

namespace Application.Chats.Queries.GetChatByUsersId;

public sealed record GetChatByUsersIdQuery(
    Contact Contact,
    HttpContext HttpContext) : IQuery<Result<ChatResponse>>;