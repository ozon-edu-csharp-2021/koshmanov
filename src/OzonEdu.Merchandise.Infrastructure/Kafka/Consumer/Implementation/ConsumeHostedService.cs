using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using CSharpCourse.Core.Lib.Events;
using MediatR;
using Microsoft.Extensions.Hosting;
using OzonEdu.Merchandise.Application.Commands.CreateMerchOrder;
using OzonEdu.Merchandise.Infrastructure.Kafka.Infrastructure;


namespace OzonEdu.Merchandise.Infrastructure.Kafka.Consumer.Implementation
{
    public class ConsumeHostedService:BackgroundService
    {
        private readonly IConsumer<string, NotificationEvent> _consumer;
        private readonly IMediator _mediator;
        
        public ConsumeHostedService(IConsumer<string, NotificationEvent> consumer, IMediator mediator)
        {
            _consumer = consumer;
            _mediator = mediator;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();
            _consumer.Subscribe("employee_notification_event");
            var serializer = new JsonSerializer<NotificationEvent>();
            BinaryFormatter bf;
            while (stoppingToken.IsCancellationRequested)
            {
                var consumeResult = _consumer.Consume(stoppingToken);
                if (consumeResult != null)
                {
                    bf = new BinaryFormatter();
                    using (var ms = new MemoryStream())
                    {
                        bf.Serialize(ms, consumeResult.Message.Value);
                        var data= ms.ToArray();
   
                        var message =
                            serializer.Deserialize(data, false, SerializationContext.Empty);
                        var createMerchCommand = new CreateMerchOrderCommand
                        {
                            //EmployeeId = message,
                            EmployeeEmail = message.EmployeeEmail
                            //MerchPackId = message.Payload
                        };
                        await _mediator.Send(createMerchCommand, stoppingToken);
                    }
                    
                 
                }

                _consumer.Commit();
            }
            _consumer.Unsubscribe();
            //return Task.CompletedTask;
        }
    }
}