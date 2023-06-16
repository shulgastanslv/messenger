using Application.Common.Abstractions.Messaging;
using Application.Messages.Queries.GetMessages;
using Domain.Entities.Contacts;

namespace Application.Messages.Queries.GetFiles;

public sealed record GetFilesQuery(Contact Sender) : IQuery<FilesResponse>;
