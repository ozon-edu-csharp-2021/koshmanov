using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.Merchandise.Application.Commands.CreateMerchOrder;
using OzonEdu.Merchandise.Application.Contracts;
using OzonEdu.Merchandise.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate;

namespace OzonEdu.Merchandise.Infrastructure.Handlers
{ 
    public class CreateMerchOrderCommandHandler:IRequestHandler<CreateMerchOrderCommand, int>
    {
        private readonly IMerchOrderRepository _merchOrderRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IStockItemService _stockService;
        public CreateMerchOrderCommandHandler(IMerchOrderRepository mOrderRepository, IEmployeeRepository employeeRepository, IStockItemService stockService)
        {
            _merchOrderRepository = mOrderRepository;
            _employeeRepository = employeeRepository;
            _stockService = stockService;
        }

        public async Task<int> Handle(CreateMerchOrderCommand request, CancellationToken cancellationToken)
        {
            if (!MerchPack.TryGetPackById(request.MerchPackType, out var merchPack))
            {
                throw new Exception($"Merch pack with id {request.MerchPackType} does not exist!");
            }

            var employee = await _employeeRepository.FindByIdAsync(request.EmloyeeId, cancellationToken);
            var email = Email.Create(request.EmloyeeEmail);
            if (employee == null)
            {
                employee = Employee.Create(request.EmloyeeId, email);
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
        
            if (!_merchOrderRepository.CheckEmployeeMerch(employee.Id, merchPack).Result)
            {
                throw new Exception($"Employee {request.EmloyeeId} already have merch pack {merchPack.Name}");
            } 
            if (!_merchOrderRepository.CheckEmployeeOrders(employee.Id, merchPack).Result)
            {
                throw new Exception($"Employee {request.EmloyeeId} already have order of {merchPack.Name}");
            }
            var newMerchOrder = MerchOrder.Create(new EmployeeId(employee.Id), merchPack); 
            await _merchOrderRepository.CreateAsync(newMerchOrder, cancellationToken);
            
            //обращение к сток апи сервису с проверкой наличия мерчпака на складе
            var res = _stockService.CheckMerchPackExist(merchPack).Result;
            //если етсь ставим в прогресс и отправляем уведомление на почту иначе резервируем и ставим статус в ожидание
            if (res)
            {
                newMerchOrder.SetInProgressStatus();
                _stockService.GetStockItem(merchPack);
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
            return newMerchOrder.Id;
        }
    }
}