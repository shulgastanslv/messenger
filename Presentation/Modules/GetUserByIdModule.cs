using Application.Users.Queries.GetUserById;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Modules;

public class GetUserByIdModule : CarterModule
{
    public GetUserByIdModule() : base("/users") { }
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/getUserById", [Authorize] async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetUserByIdQuery(id));

            return Results.Ok(result);
        });
    }
}