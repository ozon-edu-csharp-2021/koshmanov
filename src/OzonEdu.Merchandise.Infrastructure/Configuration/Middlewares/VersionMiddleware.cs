using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace OzonEdu.Merchandise.Infrastructure.Configuration.Middlewares
{
    public class VersionMiddleware
    {
        public VersionMiddleware(RequestDelegate next)
        {
            
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version?.ToString()??"no version";
            var serviceName = Assembly.GetExecutingAssembly().GetName().Name;
            var response = $"version:{version}, serviceName: {serviceName}";
            await context.Response.WriteAsync(response);
        }
    }
}