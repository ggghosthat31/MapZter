using MapZter.Entity.Models;

namespace MapZter.Contracts.Interfaces;

public interface IRepositoryManager
{
    public IEntityRepository<Place> PlaceRepository { get; }
    public Task SaveAsync();
}
