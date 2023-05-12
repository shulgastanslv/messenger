using Domain.Shared;
using MediatR;

namespace Application.Common.Interfaces;

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand
{ }

public interface ICommandHandler<TCommand, TResponce> : IRequestHandler<TCommand, Result<TResponce>>
    where TCommand : ICommand<TResponce>
{ }
