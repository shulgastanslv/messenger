using Application.Messages.Queries.GetMessages;
using Carter;
using Domain.Entities.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Modules.Messages;

public class GetMessagesModule : CarterModule
{
    public GetMessagesModule() : base("/getMessages"){}
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
       app.MapGet("", [AllowAnonymous] async (Guid userReceiver, Guid userSender, ISender sender) =>
       {
           var result = await sender.Send(new GetMessagesQuery(userReceiver, userSender));

           return Results.Ok(result.Messages);
       });
    }
}