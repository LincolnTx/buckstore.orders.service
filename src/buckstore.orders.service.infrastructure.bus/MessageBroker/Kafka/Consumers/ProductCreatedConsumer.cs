using MediatR;
using Microsoft.Extensions.Logging;
using buckstore.orders.service.application.IntegrationEvents.External;

namespace buckstore.orders.service.infrastructure.bus.MessageBroker.Kafka.Consumers
{
    public class ProductCreatedConsumer : KafkaConsumer<ProductCreatedIntegrationEvent>
    {
        public ProductCreatedConsumer(IMediator bus, ILogger<ProductCreatedConsumer> logger) : base(bus, logger)
        {
        }
    }
}