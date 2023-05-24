using Domain.Primitives.Result;
using MediatR;

namespace Application.Common.Abstractions.Messaging;

public interface ICommand<out TResponse> : IRequest<TResponse>
    where TResponse : Result
{ }

