using Application.Groups.Commands.CreateGroup;
using Application.UserGroups.Commands.CreateUserGroup;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Modules;

public record GroupName(string Name);

public class GroupModule : CarterModule
{
    public GroupModule()
        : base("/group")
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/create", async ([FromBody] GroupName name, HttpContext httpContext,
            ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new CreateGroupCommand(name.Name, httpContext), cancellationToken);

            return result.IsSuccess ? Results.Ok() : Results.BadRequest(result.Error);
        }).RequireAuthorization();

        app.MapPost("/join", async ([FromBody] Guid groupId, HttpContext httpContext,
            ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new CreateUserGroupCommand(groupId, httpContext), cancellationToken);

            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
        }).RequireAuthorization();
    }
}