using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OpenTracing;

namespace OzonEdu.Merchandise.Infrastructure.Configuration.Middlewares
{
    public class GlobalTraceMiddleware
    {
        private readonly  RequestDelegate _next;
        private readonly ITracer _tracer;

        public GlobalTraceMiddleware(RequestDelegate next, ITracer tracer)
        {
            _next = next;
            _tracer = tracer;
        }
        
        public async Task InvokeAsync(HttpContext context)
        { 
            var traceText = ("Method {method} {url}",
                context.Request.Method,
                context.Request.Path.Value).ToString();
            
            using var span = _tracer.BuildSpan(traceText)
                .StartActive();

            await _next(context);
        }
    }
}