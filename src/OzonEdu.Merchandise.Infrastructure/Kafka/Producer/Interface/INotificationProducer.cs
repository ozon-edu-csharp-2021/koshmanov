using CSharpCourse.Core.Lib.Events;

namespace OzonEdu.Merchandise.Infrastructure.Kafka.Producer.Interface
{
    public interface INotificationProducer
    {
        void Publish(NotificationEvent notificationEvent);
    }
}