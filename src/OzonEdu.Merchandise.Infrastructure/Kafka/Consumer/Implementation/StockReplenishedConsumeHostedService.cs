using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using CSharpCourse.Core.Lib.Events;
using MediatR;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Hosting;
using OzonEdu.Merchandise.Application.Commands.RecreateMerchOrder;
using OzonEdu.Merchandise.Infrastructure.Kafka.Infrastructure;

namespace OzonEdu.Merchandise.Infrastructure.Kafka.Consumer.Implementation
{
    public class StockReplenishedConsumeHostedService:BackgroundService
    {
        private readonly IConsumer<string, NotificationEvent> _consumer;
        private readonly IMediator _mediator;
        
        public StockReplenishedConsumeHostedService(IConsumer<string, NotificationEvent> consumer, IMediator mediator)
        {
            _consumer = consumer;
            _mediator = mediator;
        }
        
        /*3. Прослушивать топик "stock_replenished_event" на предмет наличия сообщения типа StockReplenishedEvent. 
При его наличии сообщения, проанализировать очередь людей на выдачу, и сделать повторный запрос на выдачу*/
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();
            _consumer.Subscribe("stock_replenished_event");
            var serializer = new JsonSerializer<CSharpCourse.Core.Lib.Events.StockReplenishedEvent>();
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
                        var data = ms.ToArray();

                        var message =
                            serializer.Deserialize(data, false, SerializationContext.Empty);
                        var recreateMerchCommand = new RecreateMerchOrderCommand {Items = new List<StockReplenishedItem>()};
                        message.Type.ToList().ForEach(x =>
                        {
                            recreateMerchCommand.Items.Add(
                                new StockReplenishedItem()
                                {
                                    Sku = x.Sku,
                                    ClothingSize = x.ClothingSize,
                                    ItemTypeId = x.ItemTypeId,
                                    ItemTypeName = x.ItemTypeName
                                });
                        });
                        await _mediator.Send(recreateMerchCommand, stoppingToken);
                    }
                }
                _consumer.Commit();
            }
            _consumer.Unsubscribe();
        }
    }
}

/*
  public class StockReplenishedEvent
{
    public IReadOnlyCollection<StockReplenishedItem> Type { get; set; }
}

public class StockReplenishedItem
{
    public long Sku { get; set; }

    public int ItemTypeId { get; set; }

    public string ItemTypeName { get; set; } = string.Empty;

    public int? ClothingSize { get; set; }
}
 */