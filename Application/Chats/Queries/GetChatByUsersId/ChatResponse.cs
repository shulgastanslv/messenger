using Domain.Entities.Chats;
using Domain.Primitives.Result;

namespace Application.Chats.Queries.GetChatByUsersId;

public sealed record ChatResponse(Chat Chat);
