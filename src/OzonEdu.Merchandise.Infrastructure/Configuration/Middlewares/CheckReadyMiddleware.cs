using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OzonEdu.Merchandise.Infrastructure.Configuration.Middlewares
{
    public class CheckReadyMiddleware
    {
        public CheckReadyMiddleware(RequestDelegate next)
        {
            
        }
        
        public async Task InvokeAsync(HttpContext context)
        {
            await context.Response.WriteAsync($"{StatusCodes.Status200OK.ToString()} Ok");
        }
    }
}