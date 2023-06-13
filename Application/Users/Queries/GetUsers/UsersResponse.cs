using Domain.Entities.Contacts;
using Domain.Primitives.Result;

namespace Application.Users.Queries.GetUsers;

public sealed record UsersResponse(Result<IEnumerable<Contact>?> Contacts);