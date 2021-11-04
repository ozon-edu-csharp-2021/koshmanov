using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate;
using OzonEdu.Merchandise.Infrastructure.Commands.FindById;

namespace OzonEdu.Merchandise.Infrastructure.Handlers
{
    public class FindMerchOrderByIdCommandHandler : IRequestHandler<FindMerchOrderByIdCommand, MerchOrder>
    {
        private readonly IMerchOrderRepository _repository;

        public FindMerchOrderByIdCommandHandler(IMerchOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<MerchOrder> Handle(FindMerchOrderByIdCommand request, CancellationToken cancellationToken)
        { 
            var result =  await _repository.FindById(request.Id, cancellationToken);
            return result;
        }
    }
}