using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OzonEdu.Merchandise.Application.Commands.CreateMerchOrder;
using OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate;


namespace OzonEdu.Merchandise.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection service)
        {
            service.AddMediatR(typeof(CreateMerchOrderCommand).Assembly);
            return service;
        }
        
    }
}