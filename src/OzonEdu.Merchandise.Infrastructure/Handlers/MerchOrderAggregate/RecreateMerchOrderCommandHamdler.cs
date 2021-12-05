using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OpenTracing;
using OzonEdu.Merchandise.Application.Commands.RecreateMerchOrder;
using OzonEdu.Merchandise.Application.Contracts;
using OzonEdu.Merchandise.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate;
using OzonEdu.Merchandise.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.Merchandise.Domain.Contracts;

namespace OzonEdu.Merchandise.Infrastructure.Handlers.MerchOrderAggregate
{
    public class RecreateMerchOrderCommandHandler : IRequestHandler<RecreateMerchOrderCommand, List<long>>
    {
        private readonly IMerchOrderRepository _merchOrderRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMerchPackRepository _merchPackRepository;
        private readonly IStockItemService _stockService;
        private readonly IUnitOfWork _unitOfWork;

        private readonly ITracer _tracer;

        public RecreateMerchOrderCommandHandler(IMerchOrderRepository mOrderRepository,
            IMerchPackRepository merchPackRepository, IEmployeeRepository employeeRepository,
            IStockItemService stockService, IUnitOfWork unitOfWork, ITracer tracer)
        {
            _merchOrderRepository = mOrderRepository;
            _merchPackRepository = merchPackRepository;
            _employeeRepository = employeeRepository;
            _stockService = stockService;
            _unitOfWork = unitOfWork;
            _tracer = tracer;
        }

        public async Task<List<long>> Handle(RecreateMerchOrderCommand request,
            CancellationToken cancellationToken)
        {
            using var span = _tracer.BuildSpan("CreateMerchOrderCommandHandler.Handle")
                .StartActive();

            await _unitOfWork.StartTransaction(cancellationToken);

            var merchOrdersInWaitingState = await _merchOrderRepository.GetAllEmployeeOrdersInSpecialStatus(
                new[] {OrderState.Waiting.Id}, cancellationToken);
            var responce = new List<long>();

            merchOrdersInWaitingState.ToList().ForEach(async x =>
            {
                var merchPack = await _merchPackRepository.GetPackByIdAsync(x.MerchPackId.Value, cancellationToken);

                var res = await _stockService.CheckMerchPackExist(merchPack);
                //если етсь ставим в прогресс и отправляем уведомление на почту иначе резервируем и ставим статус в ожидание
                if (res)
                {
                    x.SetInProgressStatus();
                    await _stockService.GetStockItem(merchPack);
                    //отправка уведомления сотруднику
                    x.SetGiveOutStatus();
                    responce.Add(x.Id);
                }
                else
                {
                    x.SetWaitingStatus();
                }

                await _merchOrderRepository.UpdateAsync(x, cancellationToken);
                await _merchOrderRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

            });
            return responce;
        }
    }
}