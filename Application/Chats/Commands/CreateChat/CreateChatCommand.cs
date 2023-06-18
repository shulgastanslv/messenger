using Application.Common.Abstractions.Messaging;
using Domain.Entities.Chats;
using Domain.Entities.Contacts;
using Domain.Primitives.Result;
using Microsoft.AspNetCore.Http;

namespace Application.Chats.Commands.CreateChat;

public sealed record CreateChatCommand(
    Contact Contact,
    HttpContext HttpContext) : ICommand<Result<Chat>>;