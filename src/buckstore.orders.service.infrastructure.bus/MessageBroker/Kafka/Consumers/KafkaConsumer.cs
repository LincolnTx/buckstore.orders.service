﻿using System;
using MediatR;
using MassTransit;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using buckstore.orders.service.application.IntegrationEvents;

namespace buckstore.orders.service.infrastructure.bus.MessageBroker.Kafka.Consumers
{
    public abstract class KafkaConsumer<TEvent> : IConsumer<TEvent> where TEvent : IntegrationEvent
    {
        private readonly IMediator _bus;
        private readonly ILogger _logger;

        protected KafkaConsumer(IMediator bus, ILogger logger)
        {
            _bus = bus;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<TEvent> context)
        {
            try
            {
                await _bus.Publish(context.Message);
            }
            catch (Exception e)
            {
                _logger.LogCritical($"Ocorreu um erro ao consumir o evento {context.Message.GetType().Name} : {e}");
                throw;
            }
        }
    }
}