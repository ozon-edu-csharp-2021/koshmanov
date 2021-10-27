using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace OzonEdu.Merchandise.Infrastructure.Configuration.Middlewares
{
    public class CheckLiveMiddleware
    {
        public CheckLiveMiddleware(RequestDelegate next)
        {
            
        }
        
        public async Task InvokeAsync(HttpContext context)
        {
            await context.Response.WriteAsync($"{StatusCodes.Status200OK.ToString()} Ok");
        }
    }
}