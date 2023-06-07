using Carter;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Application.Users.Commands.UserAuthentication;

namespace Presentation.Modules.Users;

public class AuthenticationModule : CarterModule
{
    public AuthenticationModule() : base("/authentication") { }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/auth", [AllowAnonymous] async ([FromBody] UserAuthenticationCommand request,
            ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(request, cancellationToken);

            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
        });
    }
}

