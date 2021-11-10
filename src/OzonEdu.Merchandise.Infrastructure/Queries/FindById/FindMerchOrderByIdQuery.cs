using MediatR;
using OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate;

namespace OzonEdu.Merchandise.Infrastructure.Queries.FindById
{
    public class FindMerchOrderByIdQuery:IRequest<MerchOrder>
    {
        public long Id { get; set; }
    }
}