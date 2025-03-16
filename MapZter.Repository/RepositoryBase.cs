using MapZter.Contracts.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MapZter.Repository;

public abstract class RepositoryBase<T> : IRepositoryBase<T>, IEntityRepository<T> where T : class
{
    protected RepositoryContext _repostioryContext;

	public RepositoryBase(RepositoryContext repositoryContext)
	{
		_repostioryContext = repositoryContext;
	}
	
	public RepositoryContext CurrentDatabaseContext => _repostioryContext;

	public IQueryable<T> FindAll(bool trackChanges) =>
		!trackChanges ?
			_repostioryContext.Set<T>().AsNoTracking():
			_repostioryContext.Set<T>();

	public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
		!trackChanges ?
			_repostioryContext.Set<T>().Where(expression).AsNoTracking():
			_repostioryContext.Set<T>().Where(expression);

	public void Create(T entity) =>
		_repostioryContext.Set<T>().Add(entity);

	public void Update(T newEntity)
	{
		return; //entity update strategy depends on entity repository implementation
	}

	public void Delete(T entity) =>
		_repostioryContext.Set<T>().Remove(entity);

	public async Task SaveChanges() =>
		await _repostioryContext.SaveChangesAsync();
}