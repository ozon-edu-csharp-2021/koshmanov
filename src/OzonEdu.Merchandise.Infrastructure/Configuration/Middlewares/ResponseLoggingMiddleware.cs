using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace OzonEdu.Merchandise.Infrastructure.Configuration.Middlewares
{
    public class ResponseLoggingMiddleware
    {
        private readonly  RequestDelegate _next;
        private readonly ILogger<ResponseLoggingMiddleware> _logger;
        public ResponseLoggingMiddleware(RequestDelegate next, ILogger<ResponseLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        
        public async Task InvokeAsync(HttpContext context)
        {
            if(context.Request.ContentType.Contains("grpc"))
                await _next(context);
            else
                await LogResponse(context);
        }

        private async Task LogResponse(HttpContext context)
        {
            var responseLog = new StringBuilder();
            try
            {
                responseLog.Append($"Method: {context.Request.Method};\n");
                responseLog.Append($"Path: {context.Request.Path};\n");
                responseLog.Append($"Scheme: {context.Request.Scheme};\n");
                _logger.LogInformation(responseLog.ToString());
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while logging header.");
            }
            var originalBody = context.Response.Body;
            await using var newBody = new MemoryStream();
            context.Response.Body = newBody;
            try
            {
                await _next(context);
            }
            finally
            {
                context.Response.Body.Seek(0, SeekOrigin.Begin);
                var bodyText = await new StreamReader(context.Response.Body).ReadToEndAsync();
                context.Response.Body.Seek(0, SeekOrigin.Begin);
                _logger.LogInformation($"Response body: {bodyText}");
                await newBody.CopyToAsync(originalBody);
            }
        }
    }
}