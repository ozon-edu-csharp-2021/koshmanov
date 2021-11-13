using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate;
using OzonEdu.Merchandise.Infrastructure.Queries.FindById;

namespace OzonEdu.Merchandise.Infrastructure.Handlers
{
    public class FindMerchOrderByIdQueryHandler : IRequestHandler<FindMerchOrderByIdQuery, MerchOrder>
    {
        private readonly IMerchOrderRepository _repository;
        public FindMerchOrderByIdQueryHandler(IMerchOrderRepository repository)
        {
            _repository = repository;
        }
        public async Task<MerchOrder> Handle(FindMerchOrderByIdQuery request, CancellationToken cancellationToken)
        { 
            var result =  await _repository.FindById(request.Id, cancellationToken);
            return result;
        }
    }
}