using MediatR;
using Microsoft.Extensions.Logging;
using buckstore.orders.service.application.IntegrationEvents.External;

namespace buckstore.orders.service.infrastructure.bus.MessageBroker.Kafka.Consumers
{
    public class StockConfirmationFailConsumer : KafkaConsumer<StockConfimationFailIntegrationEvent>
    {
        public StockConfirmationFailConsumer(IMediator bus, ILogger<StockConfirmationFailConsumer> logger) : base(bus, logger)
        {
        }
    }
}