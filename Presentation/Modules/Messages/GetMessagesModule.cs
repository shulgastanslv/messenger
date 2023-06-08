using Application.Messages.Queries.GetMessages;
using Carter;
using Domain.Entities.Contacts;
using Domain.Entities.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Modules.Messages;

public class GetMessagesModule : CarterModule
{
    public GetMessagesModule() : base("/message") { }
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/get", async (Contact request, ISender sender,
            HttpContext context, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new GetMessagesQuery(request, context),
                cancellationToken);

            return result.Messages.IsSuccess ?
                    Results.Ok(result.Messages.Value) :
                    Results.BadRequest(result.Messages.Error);
        });
    }
}