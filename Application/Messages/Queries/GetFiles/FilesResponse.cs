using Domain.Entities.Messages;
using Domain.Primitives.Result;

namespace Application.Messages.Queries.GetFiles;

public sealed record FilesResponse(Result<IEnumerable<Media>> Files);
