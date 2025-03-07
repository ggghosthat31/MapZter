using MapZter.Contracts.Interfaces;
using MapZter.Entity.Models;

namespace MapZter.Repository;

public class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _repostioryContext;

    private PlaceRepository _placeRepository;

    public RepositoryManager(RepositoryContext repositoryContext) =>
        _repostioryContext = repositoryContext;

    public IEntityRepository<Place> PlaceRepository
    {
        get => (_placeRepository == null) 
            ? _placeRepository = new PlaceRepository(_repostioryContext)
            : _placeRepository;
    }

    public async Task SaveAsync() =>
        await _repostioryContext.SaveChangesAsync();
}