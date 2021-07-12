namespace buckstore.orders.service.domain.SeedWork
{
	public interface IRepository<TEntity> where TEntity : IAggregateRoot
	{
		IUnitOfWorkIntegration UnitOfWork { get; }
		void Add(TEntity obj);
	}
}