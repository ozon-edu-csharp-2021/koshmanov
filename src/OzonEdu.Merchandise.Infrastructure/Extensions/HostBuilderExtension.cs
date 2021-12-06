using Confluent.Kafka;
using CSharpCourse.Core.Lib.Events;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Npgsql;
using OpenTracing;
using OzonEdu.Merchandise.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate;
using OzonEdu.Merchandise.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.Merchandise.Domain.Contracts;
using OzonEdu.Merchandise.Infrastructure.Configuration.Interceptor;
using OzonEdu.Merchandise.Infrastructure.Filters;
using OzonEdu.Merchandise.Infrastructure.Kafka.Consumer.Implementation;
using OzonEdu.Merchandise.Infrastructure.Kafka.Producer.Implementation;
using OzonEdu.Merchandise.Infrastructure.Kafka.Infrastructure;
using OzonEdu.Merchandise.Infrastructure.Kafka.Producer.Interface;
using OzonEdu.Merchandise.Infrastructure.Repositories.Implementation;
using OzonEdu.Merchandise.Infrastructure.Repositories.Infrastructure;
using OzonEdu.Merchandise.Infrastructure.Repositories.Infrastructure.Interfaces;

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
                services.AddHostedService<ConsumeHostedService>();
                services.AddScoped<INotificationProducer, EmailNotificationProducer>();
                services.AddSingleton<IProducer<string, NotificationEvent>>(
                   provider =>
                   {
                       var config = new ProducerConfig
                       {
                            BootstrapServers = "localhost:9092"
                       };

                       var prodBuilder = new ProducerBuilder<string, NotificationEvent>(config);
                       prodBuilder.SetValueSerializer(new JsonSerializer<NotificationEvent>());
                       return prodBuilder.Build();
                   });
                
                services.AddSingleton<IConsumer<string, NotificationEvent>>(
                    provider =>
                    {
                        var config = new ConsumerConfig
                        {
                            BootstrapServers = "localhost:9092",
                            GroupId = "EmployeeNotificationConsumer",
                            AutoOffsetReset = AutoOffsetReset.Earliest,
                            EnableAutoCommit = false
                        };

                        var prodBuilder = new ConsumerBuilder<string, NotificationEvent>(config);
                        prodBuilder.SetValueDeserializer(new JsonSerializer<NotificationEvent>());
                        return prodBuilder.Build();
                    });
                
                services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo{Title = "OzonEdu.Merchandise", Version = "1.0.0.1"});
                    options.CustomSchemaIds(x=>x.FullName);
                });
                services.AddGrpc(options=> options.Interceptors.Add<LoggingInterceptor>());
                services.AddSingleton<IStartupFilter, MiddlewareStartupFilter>();
                services.AddScoped<IDbConnectionFactory<NpgsqlConnection>, NpgsqlConnectionFactory>();
                services.AddScoped<IUnitOfWork, UnitOfWork>();
                services.AddScoped<IChangeTracker, ChangeTracker>();
                services.AddScoped<IMerchOrderRepository, MerchandiseRepository>();
                services.AddScoped<IMerchPackRepository, MerchPackRepository>();
                services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            });
            return builder;
        }
    }
}