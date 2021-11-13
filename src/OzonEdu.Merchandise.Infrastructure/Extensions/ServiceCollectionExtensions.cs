using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate;
using OzonEdu.Merchandise.Infrastructure.Commands.CreateMerchOrder;


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