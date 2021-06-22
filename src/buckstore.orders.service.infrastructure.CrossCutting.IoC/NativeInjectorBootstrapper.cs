using System;
using MediatR;
using AutoMapper.Configuration;
using Microsoft.Extensions.DependencyInjection;
using buckstore.orders.service.domain.Exceptions;
using buckstore.orders.service.application.IntegrationEvents;
using buckstore.orders.service.application.Adapters.MessageBroker;
using buckstore.orders.service.infrastructure.bus.MessageBroker.Kafka.Producers;

namespace buckstore.orders.service.infrastructure.CrossCutting.IoC
{
	public class NativeInjectorBootstrapper
	{
		public static void RegisterServices(IServiceCollection services)
		{
			RegisterData(services);
			RegisterMediatR(services);
			RegisterProducers(services);
		}

		public static void RegisterData(IServiceCollection services)
		{
			// here goes your repository injection
			// sample: services.AddScoped<IUserRepository, UserRepository>();
		}

		public static void RegisterMediatR(IServiceCollection services)
		{
			// injection for Mediator
			services.AddScoped<INotificationHandler<ExceptionNotification>, ExceptionNotificationHandler>();
		}

		public static void RegisterProducers(IServiceCollection services)
		{
			services.AddScoped<IMessageProducer<OrderFinishedIntegrationEvent>, KafkaProducer<OrderFinishedIntegrationEvent>>();
		}
	}
}