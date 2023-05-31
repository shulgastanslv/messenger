using Application.Users.Commands.UserRegistration;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Modules;

public class RegistrationModule : CarterModule
{
    public RegistrationModule()
        : base("/registration")
    {
    }
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/reg", [AllowAnonymous] async ([FromBody] UserRegistrationCommand request,
            ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(request, cancellationToken);

            return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result.Error);
        });
    }

}

