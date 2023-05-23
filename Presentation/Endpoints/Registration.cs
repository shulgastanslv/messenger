using Application.Users.Commands.CreateUser;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Endpoints;

public class Registration : CarterModule
{
    public Registration()
        : base("/registration")
    {
    }
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/reg", async ([FromBody] CreateUserCommand request, ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(request, cancellationToken);

            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result.Errors);
        });
    }

}

