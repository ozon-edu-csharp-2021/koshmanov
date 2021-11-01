using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace OzonEdu.Merchandise.Infrastructure.Configuration.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;
        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        
        public async Task InvokeAsync(HttpContext context)
        {
            await LogRequest(context);
            await _next(context);
        }

        private async Task LogRequest(HttpContext context)
        {
            try
            {
                if(context.Request.ContentType.Contains("grpc"))
                    return;
                var requestLog = new StringBuilder();
                requestLog.Append($"Method: {context.Request.Method};\n");
                requestLog.Append($"Path: {context.Request.Path};\n");
                requestLog.Append($"Scheme: {context.Request.Scheme};\n");
                
                if (context.Request.RouteValues.Count > 0)
                {
                    foreach (var routeKeyValue in (context.Request.RouteValues))
                        requestLog.Append($"{routeKeyValue.Key}:{routeKeyValue.Value}\n");
                }

                if (context.Request.ContentLength > 0)
                {
                    context.Request.EnableBuffering();
                    var buffer = new byte[context.Request.ContentLength.Value];
                    await context.Request.Body.ReadAsync(buffer, 0, buffer.Length);
                    var bodyAsString = Encoding.UTF8.GetString(buffer);
                    requestLog.Append($"Request body: {bodyAsString}\n");
                    context.Request.Body.Position = 0;
                }
                _logger.LogInformation($"{requestLog}");
                
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not log request.");
            }
        }
    }
}