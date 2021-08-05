using MediatR;
using Microsoft.Extensions.Logging;
using buckstore.orders.service.application.IntegrationEvents;

namespace buckstore.orders.service.infrastructure.bus.MessageBroker.Kafka.Consumers
{
    public class BuyerCreatedConsumer : KafkaConsumer<BuyerCreatedIntegrationEvent>
    {
        public BuyerCreatedConsumer(IMediator bus, ILogger<BuyerCreatedConsumer> logger) : base(bus, logger)
        {
        }
    }
}