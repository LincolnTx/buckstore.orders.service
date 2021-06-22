using MediatR;
using Microsoft.Extensions.Logging;
using buckstore.orders.service.application.IntegrationEvents;

namespace buckstore.orders.service.infrastructure.bus.MessageBroker.Kafka.Consumers
{
    public class ProductDeletedConsumer : KafkaConsumer<ProductDeletedIntegrationEvent>
    {
        public ProductDeletedConsumer(IMediator bus, ILogger<ProductDeletedConsumer> logger) : base(bus, logger)
        {
        }
    }
}