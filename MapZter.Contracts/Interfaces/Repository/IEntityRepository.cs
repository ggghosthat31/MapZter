using System.Linq.Expressions;

namespace MapZter.Contracts.Interfaces.Repository;

public interface IEntityRepository<T> : IRepository
{
    public IQueryable<T> FindAll(bool trackChanges);

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);

    public void Create(T entity);

    public void Update(T entity);

    public void Delete(T entity);
}