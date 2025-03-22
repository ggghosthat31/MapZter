using MapZter.Contracts.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MapZter.Repository;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected RepositoryContext _repositoryContext;

	public RepositoryBase(RepositoryContext repositoryContext)
	{
		_repositoryContext = repositoryContext;
	}
	
	public RepositoryContext CurrentDatabaseContext => _repositoryContext;

	public IQueryable<T> FindAll(bool trackChanges = true) =>
		!trackChanges ?
			_repositoryContext.Set<T>().AsNoTracking():
			_repositoryContext.Set<T>();

	public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false) =>
		!trackChanges ?
			_repositoryContext.Set<T>().Where(expression).AsNoTracking():
			_repositoryContext.Set<T>().Where(expression);

	public void Create(T entity) =>
		_repositoryContext.Set<T>().Add(entity);

	//entity update strategy depends on entity repository implementation
	public void Update(T newEntity)
	{}

	public void Delete(T entity) =>
		_repositoryContext.Set<T>().Remove(entity);

	public async Task SaveChanges() =>
		await _repositoryContext.SaveChangesAsync();
}