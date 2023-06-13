using Application.Users.Queries.GetUsers;
using Application.Users.Queries.GetUsersByUsername;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Modules;

public class UserModule : CarterModule
{
    public UserModule() : base("/users")
    {
    }


    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/getUsersByUsername", async (string username, ISender sender, HttpContext httpContext,
            CancellationToken cancellationToken) =>
        {
            var result = (await sender.Send(
                new GetUsersByUsernameQuery(username, httpContext),
                cancellationToken)).Users;

            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
        }).AllowAnonymous();

        app.MapGet("/get", async (ISender sender, HttpContext httpContext,
            CancellationToken cancellationToken) =>
        {
            var result = (await sender.Send(new GetUsersQuery(httpContext), cancellationToken)).Contacts;

            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
        }).AllowAnonymous();
    }
}