using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace SimpleBankApp.Application.Common.Behaviors
{
    public class RequestLoggingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : IErrorOr
    {

        private readonly ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> _logger;

        public RequestLoggingPipelineBehavior(ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            string requestName = typeof(TRequest).Name;
            _logger.LogInformation("Starting request {RequestName}", requestName);
            TResponse result = await next();
            if(result.IsError)
            {
                _logger.LogError("Request failure {RequestName}. Error: {@Error}", requestName, result.Errors); ;
            }
            else
            {
                _logger.LogInformation("Completed request {RequestName}", requestName);
            }

            return result;
        }
    }
}
