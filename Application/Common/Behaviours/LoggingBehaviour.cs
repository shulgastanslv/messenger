using Domain.Primitives.Result;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviours;

public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse> where TResponse : Result
{
    private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;

    public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
       => _logger = logger;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            $"Starting request {typeof(TRequest).Name}, {DateTime.Now}");

        var result = await next();

        if (!result.IsSuccess)
        {
            _logger.LogError(
                $"Request failure {typeof(TRequest).Name}, {result.Error} {DateTime.Now}");
        }

        _logger.LogInformation(
            $"Completed request {typeof(TRequest).Name}, {DateTime.Now}");

        return result;
    }
}