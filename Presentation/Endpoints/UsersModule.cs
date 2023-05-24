using Application.Users.Queries.GetAllUsers;
using Application.Users.Queries.GetUserById;
using Carter;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Endpoints;

public class UsersModule : CarterModule
{
    public UsersModule() : base("/users") {}
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/getAllUsers", async (ISender sender) =>
        {
            var result = await sender.Send(new GetAllUsersQuery());

            return Results.Ok(result.Users);
        });

        app.MapGet("/getUserById", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetUserByIdQuery(id));

            return Results.Ok(result);
        });
    }
}

