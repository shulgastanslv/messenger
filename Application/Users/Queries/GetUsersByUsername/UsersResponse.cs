using Domain.Entities.Contacts;
using Domain.Primitives.Result;

namespace Application.Users.Queries.GetUsersByUsername;

public sealed record UsersResponse(Result<IEnumerable<Contact>> Users);