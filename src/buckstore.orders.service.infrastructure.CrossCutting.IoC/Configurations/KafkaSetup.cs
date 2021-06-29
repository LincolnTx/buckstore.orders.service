using System;
using MassTransit;
using MassTransit.Registration;
using MassTransit.KafkaIntegration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using buckstore.orders.service.application.IntegrationEvents.External;
using buckstore.orders.service.infrastructure.environment.Configurations;
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
                        
                        k.TopicEndpoint<ProductCreatedIntegrationEvent>(_kafkaConfiguration.ManagerToOrdersCreate, _kafkaConfiguration.Group,
                            e =>
                            {
                                e.ConfigureConsumer<ProductCreatedConsumer>(ctx);
                                e.CreateIfMissing(options =>
                                {
                                    options.NumPartitions = 3;
                                    options.ReplicationFactor = 1;
                                });
                            });
                        
                        k.TopicEndpoint<ProductUpdatedIntegrationEvent>(_kafkaConfiguration.ManagerToOrdersUpdate, _kafkaConfiguration.Group,
                            e =>
                            {
                                e.ConfigureConsumer<ProductUpdatedConsumer>(ctx);
                                e.CreateIfMissing(options =>
                                {
                                    options.NumPartitions = 3;
                                    options.ReplicationFactor = 1;
                                });
                            });
                        
                        k.TopicEndpoint<ProductDeletedIntegrationEvent>(_kafkaConfiguration.ManagerToOrdersDelete, _kafkaConfiguration.Group,
                            e =>
                            {
                                e.ConfigureConsumer<ProductDeletedConsumer>(ctx);
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
            rider.AddConsumer<ProductCreatedConsumer>();
            rider.AddConsumer<ProductUpdatedConsumer>();
            rider.AddConsumer<ProductDeletedConsumer>();
        }

        static void AddProducers(this IRiderRegistrationConfigurator rider)
        {
            rider.AddProducer<OrderFinishedIntegrationEvent>(_kafkaConfiguration.OrdersToProducts);
            rider.AddProducer<OrderToManagerIntegrationEvent>(_kafkaConfiguration.OrdersToManager);
        }
    }
}