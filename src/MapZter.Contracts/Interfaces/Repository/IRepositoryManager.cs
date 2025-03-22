using MapZter.Entity.Models;

namespace MapZter.Contracts.Interfaces.Repository;

public interface IRepositoryManager
{
    public IEntityRepository<Place> PlaceRepository { get; }
    public Task SaveAsync();
}
