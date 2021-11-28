using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RestEase.HttpClientFactory;
using OzonEdu.Merchandise.Application.Contracts;
using OzonEdu.Merchandise.GrpcServices;
using OzonEdu.Merchandise.Infrastructure.Configuration.Database;
using OzonEdu.Merchandise.Infrastructure.Extensions;
using OzonEdu.Merchandise.Services;
using OzonEdu.Merchandise.Services.Interfaces;


namespace OzonEdu.Merchandise
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup), typeof(DatabaseConnectionOptions));
            services.Configure<DatabaseConnectionOptions>(Configuration.GetSection(nameof(DatabaseConnectionOptions)));
            services.AddSingleton<MerchandiseGrpcService>();
            services.AddSingleton<IMerchandiseService,MerchandiseService>();
            services.AddRestEaseClient<IStockItemService>("http://localhost:5005");
            services.AddSingleton<IStockItemService>();
            services.AddInfrastructure();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<MerchandiseGrpcService>();
                endpoints.MapControllers();
            });
        }
    }
}