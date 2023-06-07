using Application.Messages.Commands.SaveMessageCommand;
using Carter;
using Domain.Entities.Messages;
using Domain.Primitives.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Modules.Messages;

public class SaveMessageModule : CarterModule
{
    public SaveMessageModule()
        : base("/receiver")
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("", [Authorize] async (Message request, ISender sender,
            HttpContext context, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new SaveMessageCommand(request, context), cancellationToken);

            return result.IsSuccess ? Results.Ok() : Results.BadRequest(result.Error);
        });
    }
}