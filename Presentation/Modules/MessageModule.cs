using Application.Messages.Commands.SaveMessageCommand;
using Application.Messages.Queries.GetLastMessages;
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
        app.MapPost("/get", async (Contact request,
            ISender sender, CancellationToken cancellationToken) =>
        {
            var result = (await sender.Send(new GetMessagesQuery(request),
                cancellationToken)).Messages;

            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
        });

        app.MapGet("/getlast", async (DateTime lastResponseTime, ISender sender,
            HttpContext context, CancellationToken cancellationToken) =>
        {
            var result = (await sender.Send(new GetLastMessagesQuery(lastResponseTime, context),
                cancellationToken)).Messages;

            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
        }).RequireAuthorization();

        app.MapPost("/send", async (Message request, ISender sender,
            HttpContext context, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new SaveMessageCommand(request, context),
                cancellationToken);

            return result.IsSuccess ? Results.Ok() : Results.BadRequest(result.Error);
        }).RequireAuthorization();

    }
}