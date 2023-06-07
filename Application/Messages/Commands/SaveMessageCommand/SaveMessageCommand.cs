using Application.Common.Abstractions.Messaging;
using Domain.Entities.Messages;
using Domain.Primitives.Result;
using Microsoft.AspNetCore.Http;

namespace Application.Messages.Commands.SaveMessageCommand;

public sealed record SaveMessageCommand(Message Message, HttpContext Context) : ICommand<Result>;
