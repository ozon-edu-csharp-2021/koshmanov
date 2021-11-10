using System;
using System.Threading;
using System.Threading.Tasks;
using CSharpCourse.Core.Lib.Abstractions;
using CSharpCourse.Core.Lib.Events;
using MediatR;
using OzonEdu.Merchandise.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate;
using OzonEdu.Merchandise.Domain.AggregationModels.NamesAggregate;
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
            MerchPack merchPack = null; 
            if(!MerchPack.TryGetPackById(request.MerchPackType, ref merchPack))
                throw new Exception($"Merch pack with id {request.MerchPackType} does not exist!");

            //Обращение к сервису сотрудников за информацией о сотруднике по id
            //var employee = _employeeService.GetEmployee(request.EmloyeeId);
            var employee = new Employee(new FullName(new FirstName("Bob"), new SecondName(""), new Patronymic("")), new Contact(new Phone(""), new Email("Bob@mail.bob")));
            
            if (!_repository.CheckEmployeeMerch(employee, merchPack).Result)
                throw new Exception($"Employee already have merch pack {nameof(MerchPack.WelcomePack)}");

            var newMerchOrder = new MerchOrder(employee, merchPack, OrderState.New);
            var merchOrderInDb = await _repository.FindById(newMerchOrder.Id);
            if (merchOrderInDb is not null)
                throw new Exception($"Merch order with id {newMerchOrder.Id} already exist");

            merchOrderInDb = await _repository.CreateAsync(newMerchOrder, cancellationToken);
            
            //тут должно быть обращение к сток апи сервису с проверкой наличия мерчпака на складе
            bool res = true; //_stockApi.CheckMerchPackExist(merchPack)
            
            //если етсь ставим в прогресс и отправляем уведомление на почту иначе резервируем и ставим статус в ожидание
            if (res)
            {
                newMerchOrder.UpdateOrderState(OrderState.InProgress);
                //_stockApi.Get(merchPack)
                //отправка уведомления сотруднику
                newMerchOrder.UpdateOrderState(OrderState.GiveOut);
            }
            else
            {
                //_stockApi.Reserve(merchPack)
                newMerchOrder.UpdateOrderState(OrderState.Waiting);
            }

            await _repository.UpdateAsync(newMerchOrder, cancellationToken);

            await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return merchOrderInDb.Id;
        }
    }
}