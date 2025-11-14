using MediatR;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace CleanArchitecture.Application.Behaviours
{
    public class RequestResponseLogginBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : class
    {
        private ILogger<RequestResponseLogginBehaviour<TRequest, TResponse>> _logger;

        public RequestResponseLogginBehaviour(ILogger<RequestResponseLogginBehaviour<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestId = Guid.NewGuid();
            var requestJson = JsonSerializer.Serialize(request);

            _logger.LogInformation($"Handling request ${requestId}: ${requestJson}");

            var response = await next();

            var responseJson = JsonSerializer.Serialize(response);

            _logger.LogInformation($"Response for ${requestId}: ${responseJson}");

            return response;
        }
    }
}
