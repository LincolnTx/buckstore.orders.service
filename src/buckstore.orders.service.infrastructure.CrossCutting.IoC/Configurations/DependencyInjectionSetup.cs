using System;
using Microsoft.Extensions.DependencyInjection;

namespace buckstore.orders.service.infrastructure.CrossCutting.IoC.Configurations
{
	public static class DependencyInjectionSetup
	{
		public static void AddDependencyInjectionSetup(this IServiceCollection services)
		{
			if (services == null) throw new ArgumentNullException(nameof(services));

			NativeInjectorBootstrapper.RegisterServices(services);
		}
	}
}