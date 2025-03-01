using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;

namespace MapZter.Repository;

public abstract class RepositoryBase<T> where T : class
{
    protected RepositoryContext _repostioryContext;

	public RepositoryBase(RepositoryContext repositoryContext) =>
		_repostioryContext = repositoryContext;

	public IQueryable<T> FindAll(bool trackChanges) =>
		!trackChanges ?
			_repostioryContext.Set<T>()
				.AsNoTracking():
			_repostioryContext.Set<T>();

	public IQueryable<T> FindByCondition(
            Expression<Func<T, bool>> expression,
            bool trackChanges) =>
		!trackChanges ?
			_repostioryContext.Set<T>()
                .Where(expression)
                .AsNoTracking():
			_repostioryContext.Set<T>()
				.Where(expression);

	public void Create(T entity) =>
		_repostioryContext.Set<T>().Add(entity);

	public void Update(T entity) =>
		_repostioryContext.Set<T>().Update(entity);

	public void Delete(T entity) =>
		_repostioryContext.Set<T>().Remove(entity);
} 
