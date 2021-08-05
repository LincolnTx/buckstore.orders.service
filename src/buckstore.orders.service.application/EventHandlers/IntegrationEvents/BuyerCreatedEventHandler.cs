using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using buckstore.orders.service.domain.SeedWork;
using buckstore.orders.service.application.IntegrationEvents;
using buckstore.orders.service.domain.Aggregates.BuyerAggregate;

namespace buckstore.orders.service.application.EventHandlers.IntegrationEvents
{
    public class BuyerCreatedEventHandler : EventHandler<BuyerCreatedIntegrationEvent>
    {
        private readonly IUnitOfWork _uow;
        private readonly IBuyerRepository _buyerRepository;
        private readonly ILogger<BuyerCreatedEventHandler> _logger;

        public BuyerCreatedEventHandler(IUnitOfWork uow, IBuyerRepository buyerRepository, ILogger<BuyerCreatedEventHandler> logger)
        {
            _uow = uow;
            _buyerRepository = buyerRepository;
            _logger = logger;
        }

        public override async Task Handle(BuyerCreatedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            var buyer = new Buyer(notification.Id, notification.Cpf, notification.Name);
            
            _buyerRepository.Add(buyer);

            if (!await _uow.Commit())
                _logger.LogCritical($"Ocorreu um erro ao criar um novo comprador via evento id = {notification.Id}");
                
        }
    }
}