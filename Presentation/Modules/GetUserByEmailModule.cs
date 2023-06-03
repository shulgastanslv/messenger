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
        app.MapGet("/getUserByEmail", [AllowAnonymous] async (string email, ISender sender) =>
        {
            var result = await sender.Send(new GetUserByEmailQuery(email));

            return Results.Ok(result.user);
        });
    }
}