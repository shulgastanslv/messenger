using Application.Common.Abstractions.Messaging;
using Domain.Entities;
using MediatR;

namespace Application.Users.Queries.GetAllUsers;

public sealed record GetAllUsersQuery() : IQuery<UsersResponse>;