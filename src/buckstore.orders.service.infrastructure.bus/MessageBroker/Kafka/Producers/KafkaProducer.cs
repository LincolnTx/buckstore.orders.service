using System.Threading.Tasks;
using MassTransit.KafkaIntegration;
using buckstore.orders.service.application.IntegrationEvents;
using buckstore.orders.service.application.Adapters.MessageBroker;

namespace buckstore.orders.service.infrastructure.bus.MessageBroker.Kafka.Producers
{
    public class KafkaProducer<TEvent> : IMessageProducer<TEvent> where TEvent : IntegrationEvent
    {
        private readonly ITopicProducer<TEvent> _topicProducer;

        public KafkaProducer(ITopicProducer<TEvent> topicProducer)
        {
            _topicProducer = topicProducer;
        }

        public async Task Produce(TEvent message)
        {
            await _topicProducer.Produce(message);
        }
    }
}