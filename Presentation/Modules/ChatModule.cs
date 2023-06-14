using Application.Chats.Commands.CreateChat;
using Application.Chats.Queries.GetChatByUsersId;
using Carter;
using Domain.Entities.Contacts;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Modules;

public class ChatModule : CarterModule
{
    public ChatModule()
        : base("/chat")
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/create", async (Contact request, HttpContext httpContext,
            ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(
                new CreateChatCommand(request, httpContext), cancellationToken);

            return result.IsSuccess ? Results.Ok(result.Value.ChatId) : Results.BadRequest(result.Error);
        }).RequireAuthorization();

    }
}