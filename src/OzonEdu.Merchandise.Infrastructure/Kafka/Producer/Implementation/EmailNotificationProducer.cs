using CSharpCourse.Core.Lib;
using CSharpCourse.Core.Lib.Events;
using Confluent.Kafka;
using OzonEdu.Merchandise.Infrastructure.Kafka.Producer.Interface;

namespace OzonEdu.Merchandise.Infrastructure.Kafka.Producer.Implementation
{
    public class EmailNotificationProducer:INotificationProducer
    {
        private readonly IProducer<string, NotificationEvent> _producer;

        public EmailNotificationProducer(IProducer<string, NotificationEvent> producer)
        {
            _producer = producer;
        }

        // 5. При успешной выдаче мерча - отправлять событие NotificationEvent в топик "email_notification_event"
        public void Publish(NotificationEvent notificationEvent)
        {
            var message = new Message<string, NotificationEvent>();
            message.Key = notificationEvent.EmployeeEmail;
            message.Value = notificationEvent;
            _producer.Produce("email_notification_event", message);
        }
    }
}