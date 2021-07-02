using System.Threading.Tasks;
using buckstore.orders.service.application.IntegrationEvents;

namespace buckstore.orders.service.application.Adapters.MessageBroker
{
    public interface IMessageProducer<in TEvent> where TEvent : IntegrationEvent
    {
        Task Produce(TEvent message);
    }
}