using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.Merchandise.Application.Commands.CreateMerchOrder;
using OzonEdu.Merchandise.Application.Contracts;
using OzonEdu.Merchandise.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate;
using OzonEdu.Merchandise.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.Merchandise.Domain.Contracts;

namespace OzonEdu.Merchandise.Infrastructure.Handlers.MerchOrderAggregate
{ 
    public class CreateMerchOrderCommandHandler:IRequestHandler<CreateMerchOrderCommand, long>
    {
        private readonly IMerchOrderRepository _merchOrderRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMerchPackRepository _merchPackRepository;
        private readonly IStockItemService _stockService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateMerchOrderCommandHandler(IMerchOrderRepository mOrderRepository, IMerchPackRepository merchPackRepository,  IEmployeeRepository employeeRepository, IStockItemService stockService, IUnitOfWork unitOfWork)
        {
            _merchOrderRepository = mOrderRepository;
            _merchPackRepository = merchPackRepository;
            _employeeRepository = employeeRepository;
            _stockService = stockService;
            _unitOfWork = unitOfWork;
        }

        public async Task<long> Handle(CreateMerchOrderCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.StartTransaction(cancellationToken);
            var merchPack = await _merchPackRepository.GetPackByIdAsync(request.MerchPackTypeId, cancellationToken);
            if (merchPack==null)
            {
                throw new Exception($"Merch pack with id {request.MerchPackTypeId} does not exist!");
            }

            var employee = await _employeeRepository.FindByIdAsync(request.EmployeeId, cancellationToken);
            var email = Email.Create(request.EmployeeEmail);
            if (employee == null)
            {
                employee = Employee.Create(request.EmployeeId, email);
                await _employeeRepository.CreateAsync(employee, cancellationToken);
            }
            else
            {
                if (!employee.Email.Equals( email))
                {
                    employee.UpdateEmail(email);
                    await _employeeRepository.UpdateAsync(employee, cancellationToken);
                }
            }
        
            if (!await _merchOrderRepository.CheckEmployeeHaveMerch(employee.Id, merchPack.Id, cancellationToken))
            {
                throw new Exception($"Employee {request.EmployeeId} already have merch pack {merchPack.Type.Name}");
            } 
            var newMerchOrder = MerchOrder.Create(new EmployeeId(employee.Id), new PackId(merchPack.Id), new OrderDate(DateTime.Now)); 
            await _merchOrderRepository.CreateAsync(newMerchOrder, cancellationToken);
            
            //обращение к сток апи сервису с проверкой наличия мерчпака на складе
            var res = _stockService.CheckMerchPackExist(merchPack).Result;
            //если етсь ставим в прогресс и отправляем уведомление на почту иначе резервируем и ставим статус в ожидание
            if (res)
            {
                newMerchOrder.SetInProgressStatus();
                await _stockService.GetStockItem(merchPack);
                //отправка уведомления сотруднику
                newMerchOrder.SetGiveOutStatus();
            }
            else
            {
                newMerchOrder.SetWaitingStatus();
            }
            await _merchOrderRepository.UpdateAsync(newMerchOrder, cancellationToken);
            await _employeeRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            await _merchOrderRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return newMerchOrder.Id;
        }
    }
}