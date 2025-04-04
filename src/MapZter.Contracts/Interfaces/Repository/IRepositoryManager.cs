using MapZter.Entity.Models;

namespace MapZter.Contracts.Interfaces.Repository;

public interface IRepositoryManager
{
    public IEntityRepository<Place> PlaceRepository { get; }

    public IEntityRepository<GeoPoint> GeoPointRepository { get; }
    public Task SaveAsync();
}
