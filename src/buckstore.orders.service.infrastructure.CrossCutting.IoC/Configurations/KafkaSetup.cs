using System;
using MassTransit;
using MassTransit.Registration;
using MassTransit.KafkaIntegration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using buckstore.orders.service.application.IntegrationEvents;
using buckstore.orders.service.infrastructure.environment.Configuration;
using buckstore.orders.service.infrastructure.bus.MessageBroker.Kafka.Consumers;

namespace buckstore.orders.service.infrastructure.CrossCutting.IoC.Configurations
{
    public static class KafkaSetup
    {
        private static KafkaConfiguration _kafkaConfiguration;

        public static void AddKafka(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            _kafkaConfiguration = configuration.GetSection("KafkaConfiguration").Get<KafkaConfiguration>();

            services.AddMassTransit(bus =>
            {
                bus.UsingInMemory((ctx, cfg) => cfg.ConfigureEndpoints(ctx));
                
                bus.AddRider(rider =>
                {
                    rider.AddConsumer();
                    rider.AddProducers();
                    
                    rider.UsingKafka((ctx, k) =>
                    {
                        k.Host(_kafkaConfiguration.ConnectionString);
                        
                       k.TopicEndpoint<StockConfirmationIntegrationEvent>(_kafkaConfiguration.ProductsStockResponseSuccess, _kafkaConfiguration.Group,
                           e =>
                           {
                               e.ConfigureConsumer<StockConfirmationConsumer>(ctx);
                               e.CreateIfMissing(options =>
                               {
                                   options.NumPartitions = 3;
                                   options.ReplicationFactor = 1;
                               });
                           });
                       
                       k.TopicEndpoint<StockConfirmationFailConsumer>(_kafkaConfiguration.ProductsStockResponseFail, _kafkaConfiguration.Group,
                           e =>
                           {
                               e.ConfigureConsumer<StockConfirmationFailConsumer>(ctx);
                               e.CreateIfMissing(options =>
                               {
                                   options.NumPartitions = 3;
                                   options.ReplicationFactor = 1;
                               });
                           });
                    });
                });
            });

            services.AddMassTransitHostedService();
        }
        
        static void AddConsumer(this IRegistrationConfigurator rider)
        {
            rider.AddConsumer<StockConfirmationConsumer>();
            rider.AddConsumer<StockConfirmationFailConsumer>();
        }

        static void AddProducers(this IRiderRegistrationConfigurator rider)
        {
            rider.AddProducer<OrderFinishedIntegrationEvent>("order-finished"); // talvez deletar
            rider.AddProducer<OrderToManagerIntegrationEvent>(_kafkaConfiguration.OrdersToManager);
            rider.AddProducer<OrderCreatedIntegrationEvent>(_kafkaConfiguration.OrdersToProductsStockConfirmation);
        }
    }
}