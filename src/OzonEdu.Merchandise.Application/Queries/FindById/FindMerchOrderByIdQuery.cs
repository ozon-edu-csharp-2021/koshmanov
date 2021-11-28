using MediatR;
using OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate;

namespace OzonEdu.Merchandise.Application.Queries.FindById
{
    public class FindMerchOrderByIdQuery:IRequest<MerchOrder>
    {
        public int Id { get; set; }
    }
}