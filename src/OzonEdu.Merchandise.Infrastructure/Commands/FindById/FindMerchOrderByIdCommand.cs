using MediatR;
using OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate;

namespace OzonEdu.Merchandise.Infrastructure.Commands.FindById
{
    public class FindMerchOrderByIdCommand:IRequest<MerchOrder>
    {
        public long Id { get; set; }
    }
}