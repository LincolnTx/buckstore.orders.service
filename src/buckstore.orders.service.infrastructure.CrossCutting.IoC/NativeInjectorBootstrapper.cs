﻿using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using buckstore.orders.service.domain.Exceptions;
using buckstore.orders.service.application.IntegrationEvents;
using buckstore.orders.service.application.Adapters.MessageBroker;
using buckstore.orders.service.application.Adapters.Proxy.Payment;
using buckstore.orders.service.domain.Aggregates.BuyerAggregate;
using buckstore.orders.service.domain.Aggregates.OrderAggregate;
using buckstore.orders.service.domain.SeedWork;
using buckstore.orders.service.infrastructure.bus.MessageBroker.Kafka.Producers;
using buckstore.orders.service.infrastructure.Data.Repositories.BuyerRepository;
using buckstore.orders.service.infrastructure.Data.Repositories.OrderRepository;
using buckstore.orders.service.infrastructure.Data.UnitOfWork;
using buckstore.orders.service.infrastructure.environment.Configuration;
using buckstore.orders.service.infrastructure.proxy.globalPayments.Adapters;

namespace buckstore.orders.service.infrastructure.CrossCutting.IoC
{
	public class NativeInjectorBootstrapper
	{
		public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
		{
			RegisterData(services);
			RegisterMediatR(services);
			RegisterProducers(services);
			RegisterEnvironment(services, configuration);
			RegistersApplicationServices(services);
		}

		public static void RegisterData(IServiceCollection services)
		{
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<IOrderRepository, OrderRepository>();
			services.AddScoped<IBuyerRepository, BuyerRepository>();
		}

		public static void RegisterMediatR(IServiceCollection services)
		{
			// injection for Mediator
			services.AddScoped<INotificationHandler<ExceptionNotification>, ExceptionNotificationHandler>();
		}

		public static void RegisterProducers(IServiceCollection services)
		{
			services.AddScoped<IMessageProducer<OrderFinishedIntegrationEvent>, KafkaProducer<OrderFinishedIntegrationEvent>>();
			services.AddScoped<IMessageProducer<OrderToManagerIntegrationEvent>, KafkaProducer<OrderToManagerIntegrationEvent>>();
			services.AddScoped<IMessageProducer<OrderCreatedIntegrationEvent>, KafkaProducer<OrderCreatedIntegrationEvent>>();
			services.AddScoped<IMessageProducer<StockUpdateIntegrationEvent>, KafkaProducer<StockUpdateIntegrationEvent>>();
			services.AddScoped<IMessageProducer<OrderRollbackIntegrationEvent>, KafkaProducer<OrderRollbackIntegrationEvent>>();
		}

		public static void RegisterEnvironment(IServiceCollection services, IConfiguration configuration)
		{
			services.AddSingleton(configuration.GetSection("KafkaConfiguration").Get<KafkaConfiguration>());
			services.AddSingleton(configuration.GetSection("FkdPayConfiguration").Get<FkdPayConfiguration>());
		}
		public static void RegistersApplicationServices(IServiceCollection services)
		{
			// configurar CB olhando da BFF
			services.AddHttpClient<IPaymentService, FkdPaymentsService>();
		}
	}
}