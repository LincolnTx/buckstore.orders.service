using MediatR;
using Microsoft.Extensions.Logging;
using buckstore.orders.service.application.IntegrationEvents.External;

namespace buckstore.orders.service.infrastructure.bus.MessageBroker.Kafka.Consumers
{
    public class StockConfirmationConsumer : KafkaConsumer<StockConfirmationIntegrationEvent>
    {
        public StockConfirmationConsumer(IMediator bus, ILogger<StockConfirmationConsumer> logger) : base(bus, logger)
        {
        }
    }
}