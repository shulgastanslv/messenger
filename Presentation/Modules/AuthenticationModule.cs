using Application.Users.Commands.UserAuthentication;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Modules;

public class AuthenticationModule : CarterModule
{
    public AuthenticationModule() : base("/authentication")
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/auth", async ([FromBody] UserAuthenticationCommand request,
            ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(request, cancellationToken);

            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
        }).AllowAnonymous();


        app.MapPost("/confirm", () => { }).RequireAuthorization();
    }
}