﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OzonEdu.Merchandise.Infrastructure.Configuration.Interceptor;
using OzonEdu.Merchandise.Infrastructure.Filters;

namespace OzonEdu.Merchandise.Infrastructure.Extensions
{
    public static class HostBuilderExtension
    {
        public static IHostBuilder AddInfrastructure(this IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddControllers(options => options.Filters.Add<GlobalExceptionFilter>());
                services.AddSingleton<IStartupFilter, SwaggerStartupFilter>();
                services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo{Title = "OzonEdu.Merchandise", Version = "1.0.0.1"});
                    options.CustomSchemaIds(x=>x.FullName);
                });
                services.AddGrpc(options=> options.Interceptors.Add<LoggingInterceptor>());
                services.AddSingleton<IStartupFilter, MiddlewareStartupFilter>();
            });
            return builder;
        }
    }
}