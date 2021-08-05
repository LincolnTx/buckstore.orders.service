using System;
using System.Threading;
using System.Threading.Tasks;

namespace buckstore.orders.service.domain.SeedWork
{
	public interface IUnitOfWork
	{
		Task<bool> Commit();
	}

	public interface IUnitOfWorkIntegration
	{
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
		Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken));
	}
}