﻿using MediatR;
using System.Threading;
using System.Threading.Tasks;
using buckstore.orders.service.application.DTOs;
using buckstore.orders.service.application.IntegrationEvents;
using Microsoft.Extensions.Logging;
using buckstore.orders.service.domain.Events;
using buckstore.orders.service.domain.Aggregates.OrderAggregate;

namespace buckstore.orders.service.application.EventHandlers.DomainEvents.OrderCreated
{
    public class BuyerAndPaymentMethodVerifiedEventHandler : EventHandler<BuyerAndPaymentMethodVerifiedDomainEvent>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<BuyerAndPaymentMethodVerifiedEventHandler> _logger;
        private readonly IMediator _bus;

        public BuyerAndPaymentMethodVerifiedEventHandler(IOrderRepository orderRepository, 
            ILogger<BuyerAndPaymentMethodVerifiedEventHandler> logger, IMediator bus)
        {
            _orderRepository = orderRepository;
            _logger = logger;
            _bus = bus;
        }

        public override async Task Handle(BuyerAndPaymentMethodVerifiedDomainEvent notification, CancellationToken cancellationToken)
        {
            var orderToUpdate = await _orderRepository.FindById(notification.OrderId);

            orderToUpdate.SetPaymentId(notification.Payment.Id);
            
            _logger.LogInformation("Adicionando id do pagamento");

            if (notification.IsNewPaymentMethod)
            {
                var registerPaymentIntegrationEvent = new RegisterCardIntegrationEvent(new RegisterCreditCarPaymentDto
                (
                     notification.Payment.CardNumber,
                     notification.Payment.Expiration,
                     notification.Payment.Cvv,
                     notification.Payment.CardHolderName,
                     0m
                ));
                
                await _bus.Publish(registerPaymentIntegrationEvent, cancellationToken);
            }

        }
    }
}