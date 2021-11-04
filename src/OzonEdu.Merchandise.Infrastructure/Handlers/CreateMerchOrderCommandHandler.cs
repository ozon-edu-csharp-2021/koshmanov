using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate;
using OzonEdu.Merchandise.Infrastructure.Commands.CreateMerchOrder;

namespace OzonEdu.Merchandise.Infrastructure.Handlers
{
    public class CreateMerchOrderCommandHandler:IRequestHandler<CreateMerchOrderCommand, int>
    {
        private readonly IMerchOrderRepository _repository;

        public CreateMerchOrderCommandHandler(IMerchOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateMerchOrderCommand request, CancellationToken cancellationToken)
        {
            var newMerchOrder =
                new MerchOrder(request.Emloyee, request.ItemList, request.MerchManager, MerchType.Greeting);
            var merchOrderInDb = await _repository.FindById(newMerchOrder.Id);
            if (merchOrderInDb is not null)
                throw new Exception($"Merch order with id {newMerchOrder.Id} already exist");
            
            merchOrderInDb = await _repository.CreateAsync(newMerchOrder, cancellationToken);
            await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return merchOrderInDb.Id;
        }
    }
}