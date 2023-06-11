using Application.Messages.Commands.SaveMessageCommand;
using Application.Messages.Queries.GetLastMessagesAsync;
using Application.Messages.Queries.GetMessages;
using Carter;
using Domain.Entities.Contacts;
using Domain.Entities.Messages;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Modules;

public class MessageModule : CarterModule
{
    public MessageModule()
        : base("/message")
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/get", async (Contact request, ISender sender,
            HttpContext Context, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new GetMessagesQuery(request, Context),
                cancellationToken);

            return result.Messages.IsSuccess
                ? Results.Ok(result.Messages.Value)
                : Results.BadRequest(result.Messages.Error);
        });

        app.MapPost("/getlast", async (Contact request, ISender sender,
            HttpContext Context, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new GetLastMessagesQuery(request, Context),
                cancellationToken);

            return result.Messages.IsSuccess
                ? Results.Ok(result.Messages.Value)
                : Results.BadRequest(result.Messages.Error);
        });

        app.MapPost("/send", [Authorize] async (Message request, ISender sender,
            HttpContext context, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new SaveMessageCommand(request, context),
                cancellationToken);

            return result.IsSuccess ? Results.Ok() : Results.BadRequest(result.Error);
        });
    }
}