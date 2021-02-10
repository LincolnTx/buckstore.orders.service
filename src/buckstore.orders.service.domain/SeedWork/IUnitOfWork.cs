using System;
using System.Threading.Tasks;

namespace buckstore.orders.service.domain.SeedWork
{
	public interface IUnitOfWork
	{
		Task<bool> Commit();
	}
}