using MapZter.Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace MapZter.Repository;

public class PlaceTagRepository : RepositoryBase<PlaceTag>
{
    public PlaceTagRepository(RepositoryContext repositoryContext) : base(repositoryContext)
	{}

	public async Task<IEnumerable<PlaceTag>> GetAllPlaceTagsAsync() =>
		await FindAll().OrderBy(c => c.PlaceTagId)
		    .ToListAsync();

	public async Task<PlaceTag?> FindPlaceTagAsync(long placeTagId) =>
		await _repositoryContext.PlaceTags.FindAsync(placeTagId);

	public async Task<PlaceTag?> GetPlaceTagAsync(long placeTagId) =>
		await FindByCondition(x => x.PlaceTagId == placeTagId)
		    .SingleOrDefaultAsync();

	public async Task<IEnumerable<PlaceTag>> GetPlaceTagsAsync(IEnumerable<long> placeTagsId) =>
		await FindByCondition(x => placeTagsId.Contains(x.PlaceTagId))
		    .ToListAsync();

	public async Task Create(PlaceTag placeTag)
	{
		base.Create(placeTag);
		await SaveChanges();
	}

	public async Task Update(PlaceTag updatePlaceTag)
	{
		var existingPlaceTag = await _repositoryContext.PlaceTags.FindAsync(updatePlaceTag.PlaceTagId);

        if (existingPlaceTag == null)
            return;

        _repositoryContext.Entry(existingPlaceTag).CurrentValues.SetValues(updatePlaceTag);
		await SaveChanges();
	}

	public async Task Delete(long placeTagId)
	{
		var existingPlaceTag = await _repositoryContext.PlaceTags.FindAsync(placeTagId);

        if (existingPlaceTag == null)
            return;

		base.Delete(existingPlaceTag);
		await SaveChanges();
	}
}