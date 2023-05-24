using Application.Users.Commands.AuthenticateUser;
using Application.Users.Commands.CreateUser;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Endpoints;

public class Authenticate : CarterModule
{
    public Authenticate() : base("/authenticate"){}
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/auth", async ([FromBody] AuthenticateUserCommand request, ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(request, cancellationToken);

            return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result.Error);
        });
    }
}

