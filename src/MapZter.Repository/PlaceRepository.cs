using MapZter.Entity.Models;
using MapZter.Entities.RequestFeatures;
using MapZter.Repository.Extensions;
using Microsoft.EntityFrameworkCore;

namespace MapZter.Repository;

public class PlaceRepository : RepositoryBase<Place>
{
    public PlaceRepository(RepositoryContext repositoryContext) : base(repositoryContext)
	{}

	public async Task<IEnumerable<Place>> GetAllPlacesAsync() =>
		await FindAll().OrderBy(c => c.PlaceId)
		    .ToListAsync();

	public async Task<Place?> TryFindPlace(long placeId) =>
		await _repositoryContext.Places.FindAsync(placeId);

	public async Task<Place?> GetPlaceAsync(long placeId) =>
		await FindByCondition(x => x.PlaceId == placeId)
		    .SingleOrDefaultAsync();

	public async Task<IEnumerable<Place>> GetPlacesAsync(IEnumerable<long> placeIds) =>
		await FindByCondition(x => placeIds.Contains(x.PlaceId))
		    .ToListAsync();

	public async Task<IEnumerable<Place>> GetPlacesAsync(PlaceParameters placeParameters) =>
		await FindAll()
            .FilterPlaces(placeParameters)
            .Search(placeParameters.SearchTerm)
            .Sort(placeParameters.OrderBy)
            .ToListAsync();

	public async Task Create(Place place)
	{
		base.Create(place);
		await SaveChanges();
	}

	public async Task Update(Place updatePlace)
	{
		var existingPlace = await _repositoryContext.Places.FindAsync(updatePlace.PlaceId);

        if (existingPlace == null)
            return;

        _repositoryContext.Entry(existingPlace).CurrentValues.SetValues(updatePlace);
		await SaveChanges();
	}

	public async Task Delete(long placeId)
	{
		var existingPlace = await _repositoryContext.Places.FindAsync(placeId);

        if (existingPlace == null)
            return;

		base.Delete(existingPlace);
		await SaveChanges();
	}
}