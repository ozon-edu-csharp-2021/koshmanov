using System.Threading;
using System.Threading.Tasks;
using CSharpCourse.Core.Lib.Enums;
using CSharpCourse.Core.Lib.Events;
using MediatR;
using OzonEdu.Merchandise.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.Merchandise.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.Merchandise.Domain.Events;
using OzonEdu.Merchandise.Infrastructure.Kafka.Producer.Interface;

namespace OzonEdu.Merchandise.Infrastructure.Handlers.Event
{
   public sealed class OrderStateChangedToGiveOutHandler:INotificationHandler<OrderStateChangedToGiveOutEvent>
   {
      private readonly INotificationProducer _producer;
      private readonly IEmployeeRepository _employeeRepository;
      private readonly IMerchPackRepository _merchPackRepository;
      
      public OrderStateChangedToGiveOutHandler(INotificationProducer producer, IEmployeeRepository employeeRepository, IMerchPackRepository merchPackRepository)
      {
         _producer = producer;
         _employeeRepository = employeeRepository;
         _merchPackRepository = merchPackRepository;
      }
      
      public async Task Handle(OrderStateChangedToGiveOutEvent notification, CancellationToken cancellationToken)
      {
         var employee = await _employeeRepository.FindByIdAsync(notification.EmployeeId.Value, cancellationToken);
         var merchPack = await _merchPackRepository.GetPackByIdAsync(notification.MerchPackId.Value, cancellationToken);
         var mailNotify = new NotificationEvent
         {
            EmployeeName = notification.EmployeeId.ToString(),
            EmployeeEmail = employee.Email.Value,
            Payload = merchPack.Type.Id
         };
         _producer.Publish(mailNotify);
      }
   }
}