using System;
using buckstore.orders.service.infrastructure.Data.Context;
using Microsoft.Extensions.DependencyInjection;

namespace buckstore.orders.service.infrastructure.CrossCutting.IoC.Configurations
{
	public static class DatabaseSetup
	{
		public static void AddDatabaseSetup(this IServiceCollection services)
		{
			if (services == null) throw new ArgumentNullException(nameof(services));

			services.AddDbContext<ApplicationDbContext>(ServiceLifetime.Scoped);
		}
	}
}