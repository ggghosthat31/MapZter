using MapZter.Entity.Models;
using MapZter.Entities.RequestFeatures;
using MapZter.Repository.Extensions;
using Microsoft.EntityFrameworkCore;

namespace MapZter.Repository;

public class PlaceRepository : RepositoryBase<Place>
{
    public PlaceRepository(RepositoryContext repositoryContext) : base(repositoryContext)
	{}

	public async Task<IEnumerable<Place>> GetAllPlacesAsync(bool trackChanges) =>
		await FindAll(trackChanges).OrderBy(c => c.PlaceId)
		    .ToListAsync();

	public async Task<IEnumerable<Place>> GetByIdsAsync(IEnumerable<long> placeIds, bool trackChanges) =>
		await FindByCondition(x => placeIds.Contains(x.PlaceId), trackChanges)
		    .ToListAsync();

	public async Task<Place?> GetPlaceAsync(long placeId, bool trackChanges) =>
		await FindByCondition(c => c.PlaceId.Equals(placeId), trackChanges)
		    .SingleOrDefaultAsync();

	public async Task<IEnumerable<Place>> GetPlacesAsync(PlaceParameters placeParameters, bool trackChanges) =>
		await FindAll(trackChanges)
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
		var existingPlace = await _repostioryContext.Places.FindAsync(updatePlace.Id);

        if (existingPlace == null)
            return;

        _repostioryContext.Entry(existingPlace).CurrentValues.SetValues(updatePlace);
		await SaveChanges();
	}

	public async Task Delete(Place place)
	{
		var retrievedPlace = await FindByCondition(c => c.PlaceId.Equals(place.PlaceId), true).SingleOrDefaultAsync();

		if (retrievedPlace != null)
		{
			base.Delete(retrievedPlace);
			await SaveChanges();
		}
	}
}