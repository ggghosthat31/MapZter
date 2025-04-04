using MapZter.Entity.Models;
using MapZter.Entities.RequestFeatures;
using MapZter.Repository.Extensions;
using Microsoft.EntityFrameworkCore;

namespace MapZter.Repository;

public class GeoPointRepository : RepositoryBase<GeoPoint>
{
    public GeoPointRepository(RepositoryContext repositoryContext) : base(repositoryContext)
	{}

	public async Task<IEnumerable<GeoPoint>> GetAllGeoPointsAsync() =>
		await FindAll().OrderBy(c => c.Id)
		    .ToListAsync();

	public async Task<GeoPoint?> FindGeoPointAsync(long geoPointId) =>
		await _repositoryContext.GeoPoints.FindAsync(geoPointId);

	public async Task<GeoPoint?> GetGeoPointAsync(long geoPointId) =>
		await FindByCondition(x => x.Id == geoPointId)
		    .SingleOrDefaultAsync();

	public async Task<IEnumerable<GeoPoint>> GetGeoPointsAsync(IEnumerable<long> geoPointsId) =>
		await FindByCondition(x => geoPointsId.Contains(x.Id))
		    .ToListAsync();

	public async Task<IEnumerable<GeoPoint>> GetGeoPointsAsync(GeoPointParameters placeParameters) =>
		await FindAll()
            .FilterPlaces(placeParameters)
            .Search(placeParameters.SearchTerm)
            .ToListAsync();

	public async Task Create(GeoPoint place)
	{
		base.Create(place);
		await SaveChanges();
	}

	public async Task Update(GeoPoint updatePlace)
	{
		var existingPlace = await _repositoryContext.Places.FindAsync(updatePlace.Id);

        if (existingPlace == null)
            return;

        _repositoryContext.Entry(existingPlace).CurrentValues.SetValues(updatePlace);
		await SaveChanges();
	}

	public async Task Delete(long placeId)
	{
		var existingPlace = await _repositoryContext.GeoPoints.FindAsync(placeId);

        if (existingPlace == null)
            return;

		base.Delete(existingPlace);
		await SaveChanges();
	}
}