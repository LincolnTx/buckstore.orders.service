using MediatR;
using Microsoft.Extensions.Logging;
using buckstore.orders.service.application.IntegrationEvents;

namespace buckstore.orders.service.infrastructure.bus.MessageBroker.Kafka.Consumers
{
    public class ProductUpdatedConsumer : KafkaConsumer<ProductUpdatedIntegrationEvent>
    {
        public ProductUpdatedConsumer(IMediator bus, ILogger<ProductUpdatedConsumer> logger) : base(bus, logger)
        {
        }
    }
}