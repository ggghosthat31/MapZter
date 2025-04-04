using MapZter.Contracts.Interfaces.Repository;
using MapZter.Entity.Models;

namespace MapZter.Repository;

public class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _repostioryContext;

    private PlaceRepository _placeRepository;

    private GeoPointRepository _geoPointRepository;

    public RepositoryManager(RepositoryContext repositoryContext) =>
        _repostioryContext = repositoryContext;

    public IEntityRepository<Place> PlaceRepository
    {
        get => (_placeRepository == null) 
            ? _placeRepository = new PlaceRepository(_repostioryContext)
            : _placeRepository;
    }

    public IEntityRepository<GeoPoint> GeoPointRepository
    {
        get => (_geoPointRepository == null) 
            ? _geoPointRepository = new GeoPointRepository(_repostioryContext)
            : _geoPointRepository;
    }

    public async Task SaveAsync() =>
        await _repostioryContext.SaveChangesAsync();
}