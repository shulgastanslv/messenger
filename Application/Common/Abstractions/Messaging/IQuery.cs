using MediatR;

namespace Application.Common.Abstractions.Messaging;

public interface IQuery<out TResponse> : IRequest<TResponse>
{ }