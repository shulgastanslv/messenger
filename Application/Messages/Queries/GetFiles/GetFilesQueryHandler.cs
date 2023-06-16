using Application.Common.Abstractions.Messaging;
using Application.Messages.Queries.GetMessages;
using Domain.Entities.Messages;
using Domain.Primitives.Errors;
using Domain.Primitives.Result;

namespace Application.Messages.Queries.GetFiles;

public class GetFilesQueryHandler : IQueryHandler<GetFilesQuery, FilesResponse>
{
    private readonly IMessageRepository _messageRepository;

    public GetFilesQueryHandler(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<FilesResponse> Handle(GetFilesQuery request, CancellationToken cancellationToken)
    {
        if (!request.Sender.ChatId.HasValue)
            return new FilesResponse(Result.Failure<IEnumerable<Media>>(
                new Error("Chat not exist")));

        return new FilesResponse(await _messageRepository.GetFilesAsync(
            request.Sender.ChatId.Value, cancellationToken));
    }
}