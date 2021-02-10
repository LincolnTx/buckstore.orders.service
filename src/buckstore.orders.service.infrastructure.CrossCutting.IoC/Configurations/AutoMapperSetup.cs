using System;
using AutoMapper;
using buckstore.orders.service.application.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace buckstore.orders.service.infrastructure.CrossCutting.IoC.Configurations
{
	public static class AutoMapperSetup
	{
		public static void AddAutoMapper(this IServiceCollection services)
		{
			if (services == null) throw new ArgumentNullException(nameof(services));

			services.AddAutoMapper(typeof(MappingProfile));
		}
	}
}