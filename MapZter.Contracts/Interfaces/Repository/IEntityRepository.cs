namespace MapZter.Contracts.Interfaces.Repository;

public interface IEntityRepository<T> : IRepository
{
    public void Create(T entity);

    public void Update(T entity);

    public void Delete(T entity);
}