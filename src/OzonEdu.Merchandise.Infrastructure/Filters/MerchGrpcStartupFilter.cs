using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using OzonEdu.Merchandise.Infrastructure.GrpcServices;
namespace OzonEdu.Merchandise.Infrastructure.Filters
{
    public class MerchGrpcStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapGrpcService<MerchandiseGrpcService>();
                });
            };
        }
    }
}