using Application.Users.Queries.GetAllUsers;
using Application.Users.Queries.GetUserById;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Modules;

public class GetAllUsersModule : CarterModule
{
    public GetAllUsersModule() : base("/users") {}
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/getAllUsers", [AllowAnonymous] async (ISender sender) =>
        {
            var result = await sender.Send(new GetAllUsersQuery());

            return Results.Ok(result.Users);

        });
    }
}

