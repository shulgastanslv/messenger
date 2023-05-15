using Application.Common.Interfaces;
using Domain.Entities;

namespace Application.Users.Queries.FindUser;

public sealed record FindUserQuery(string email, string password) : IQuery<User>
{

}
