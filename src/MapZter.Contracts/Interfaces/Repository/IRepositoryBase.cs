namespace MapZter.Contracts.Interfaces.Repository;

public interface IRepositoryBase<T> : IEntityRepository<T> where T : class;
