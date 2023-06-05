using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Application.Users.Commands;
using Application.Users.Commands.UserAuthentication;
using Application.Users.Commands.UserEmailVerification;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Modules;

public class EmailVerificationModule : CarterModule
{
    public EmailVerificationModule() : base("emailVerification"){}
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/", [AllowAnonymous] async ([FromBody] UserEmailVerificationCommand request,
            ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(request, cancellationToken);

            return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result.Error);
        });
    }
}