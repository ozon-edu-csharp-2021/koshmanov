using System.Text.Json;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace OzonEdu.Merchandise.Infrastructure.Configuration.Interceptor
{
    
    public class LoggingInterceptor : global::Grpc.Core.Interceptors.Interceptor
    {
        private readonly ILogger<LoggingInterceptor> _logger;

        public LoggingInterceptor(ILogger<LoggingInterceptor> logger)
        {
            _logger = logger;
        }

        public override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request,
            ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation)
        {
            var requestJson = JsonSerializer.Serialize(request);
            _logger.LogInformation(requestJson);
            
            var response = base.UnaryServerHandler(request, context, continuation);

            var responseJson = JsonSerializer.Serialize(response);
            _logger.LogInformation(responseJson);
            
            return response;
        }
    }
}