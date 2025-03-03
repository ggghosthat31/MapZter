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
		await FindAll(trackChanges)
		    .OrderBy(c => c.PlaceId)
		    .ToListAsync();

	public async Task<IEnumerable<Place>> GetByIdsAsync(IEnumerable<long> placeIds, bool trackChanges) => 
		await FindByCondition(x => placeIds.Contains(x.PlaceId), trackChanges)
		    .ToListAsync();

	public async Task<Place> GetPlaceAsync(long placeId, bool trackChanges) =>
		await FindByCondition(c => c.PlaceId.Equals(placeId), trackChanges)
		    .SingleOrDefaultAsync();

	public async Task<IEnumerable<Place>> GetPlacesAsync(PlaceParameters placeParameters, bool trackChanges)
	{
		var companies = await FindAll(trackChanges)
            .FilterPlaces(placeParameters)
            .Search(placeParameters.SearchTerm)
            .Sort(placeParameters.OrderBy)
            .ToListAsync();

		return companies;
	}

	public void CreatePlace(Place place)
	{
		Create(place);
		ApplyChanges().Wait();
	}

	public void DeletePlace(Place place)
	{
		Delete(place);
		ApplyChanges().Wait();
	}
}