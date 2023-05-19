using Domain.Shared;
using MediatR;

namespace Application.Common.Interfaces;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>{}
