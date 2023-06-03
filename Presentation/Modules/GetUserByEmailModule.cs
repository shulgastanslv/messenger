using Application.Users.Queries.GetUserByEmail;
using Application.Users.Queries.GetUserById;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Modules;

public class GetUserByEmailModule : CarterModule
{
    public GetUserByEmailModule() : base("/users") { }
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/getUserByEmail", [Authorize] async (string email, ISender sender) =>
        {
            var user = await sender.Send(new GetUserByEmailQuery(email));

            return Results.Ok(user);
        });
    }
}