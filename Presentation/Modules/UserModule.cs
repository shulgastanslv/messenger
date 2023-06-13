using Carter;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Modules;

public class UserModule : CarterModule
{
    public UserModule() : base("/users")
    {
    }


    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        //app.MapGet("/getUserByUserName", [AllowAnonymous] async (string username, ISender sender) =>
        //{
        //    var result = await sender.Send(new GetUserByUserNameQuery(username));

        //    return Results.Ok(result.user);
        //});

        //app.MapPost("/update", [Authorize] async (User user, ISender sender) =>
        //{
        //    var result = await sender.Send(new UserUpdateCommand(user));

        //    return Results.Ok(result);
        //});

        //app.MapGet("/getAllUsers", [AllowAnonymous] async (ISender sender) =>
        //{
        //    var result = await sender.Send(new GetUsersQuery());

        //    return Results.Ok(result.Users);
        //});

        //app.MapGet("/getUserById", [Authorize] async (Guid id, ISender sender) =>
        //{
        //    var result = await sender.Send(new GetUserByIdQuery(id));

        //    return Results.Ok(result);
        //});
    }
}