using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OpenTracing;
using OzonEdu.Merchandise.Application.Queries.FindById;
using OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate;

namespace OzonEdu.Merchandise.Infrastructure.Handlers.MerchOrderAggregate
{
    public class FindMerchOrderByIdQueryHandler : IRequestHandler<FindMerchOrderByIdQuery, MerchOrder>
    {
        private readonly IMerchOrderRepository _repository;
        private readonly ITracer _tracer;
        public FindMerchOrderByIdQueryHandler(IMerchOrderRepository repository, ITracer tracer)
        {
            _repository = repository;
            _tracer = tracer;
        }
        public async Task<MerchOrder> Handle(FindMerchOrderByIdQuery request, CancellationToken cancellationToken)
        { 
            using var span = _tracer.BuildSpan("FindMerchOrderByIdQueryHandler.Handle")
                .StartActive();

            var result =  await _repository.FindById(request.Id, cancellationToken);
            return result;
        }
    }
}