namespace MapZter.Contracts.Interfaces;

public interface IEntityRepository<T> : IRepository
{
    public void Create(T entity);

    public void Update(T entity, T updatedEntity);

    public void Delete(T entity);
}