using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.Merchandise.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate;
using OzonEdu.Merchandise.Domain.AggregationModels.Names;
using OzonEdu.Merchandise.Infrastructure.Commands.CreateMerchOrder;

namespace OzonEdu.Merchandise.Infrastructure.Handlers
{ 
    public class CreateMerchOrderCommandHandler:IRequestHandler<CreateMerchOrderCommand, int>
    {
        private readonly IMerchOrderRepository _merchOrderRepository;
        private readonly IEmployeeRepository _employeeRepository;
        public CreateMerchOrderCommandHandler(IMerchOrderRepository mOrderRepository, IEmployeeRepository employeeRepository)
        {
            _merchOrderRepository = mOrderRepository;
            _employeeRepository = employeeRepository;
        }
        public async Task<int> Handle(CreateMerchOrderCommand request, CancellationToken cancellationToken)
        {
            if (!MerchPack.TryGetPackById(request.MerchPackType, out var merchPack))
            {
                throw new Exception($"Merch pack with id {request.MerchPackType} does not exist!");
            }
            //Обращение к сервису сотрудников за информацией о сотруднике 
            //var notMyEmployee = _employeeService.GetEmployee(request.EmployeeId);
            var employee = new Employee(new FullName(new FirstName("Bob"), new SecondName(""), new Patronymic("")), new Contact(new Phone(""), new Email("Bob@mail.bob")), new IsNotified(false));

            Employee employeeFromRep = await _employeeRepository.FindByIdAsync(employee.Id, cancellationToken);
            if (employeeFromRep is null)
            {
                await _employeeRepository.CreateAsync(employee, cancellationToken);
            }
            else
            {
                employeeFromRep = employee;
            }
            if (!_merchOrderRepository.CheckEmployeeMerch(employee.Id, merchPack).Result)
            {
                throw new Exception($"Employee {request.EmloyeeId} already have merch pack {nameof(MerchPack.WelcomePack)}");
            }
            var newMerchOrder = new MerchOrder(new EmployeeId(employeeFromRep.Id), merchPack);
            var merchOrderInDb = await _merchOrderRepository.FindById(newMerchOrder.Id, cancellationToken);
            if (merchOrderInDb is not null)
            {
                throw new Exception($"Merch order with id {newMerchOrder.Id} already exist");
            }
            merchOrderInDb = await _merchOrderRepository.CreateAsync(newMerchOrder, cancellationToken);
            
            //тут должно быть обращение к сток апи сервису с проверкой наличия мерчпака на складе
            var res = true; //_stockApi.CheckMerchPackExist(merchPack)
            //если етсь ставим в прогресс и отправляем уведомление на почту иначе резервируем и ставим статус в ожидание
            if (res)
            {
                newMerchOrder.SetInProgressStatus();
                //_stockApi.Get(merchPack)
                //отправка уведомления сотруднику
                newMerchOrder.SetGiveOutStatus();
            }
            else
            {
                //_stockApi.Reserve(merchPack)
                newMerchOrder.SetWaitingStatus();
            }
            await _merchOrderRepository.UpdateAsync(newMerchOrder, cancellationToken);
            await _employeeRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            await _merchOrderRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return merchOrderInDb.Id;
        }
    }
}