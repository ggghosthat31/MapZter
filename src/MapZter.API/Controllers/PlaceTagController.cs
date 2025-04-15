using MapZter.Entity.Models;
using Microsoft.AspNetCore.Mvc;
using MapZter.Proxy;
using MapZter.Proxy.Interactions;
using MapZter.Entities.RequestFeatures;

namespace MapZter.API;

[Route("api/[controller]")]
[ApiController]
public class PlaceTagControler : ControllerBase
{
    private readonly RepositoryProxy _repositoryProxy;

    public PlaceTagControler(RepositoryProxy repositoryProxy)
    {
        _repositoryProxy = repositoryProxy;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPlaceTags()
    {
        var queryResult = await _repositoryProxy.QueryPlaceTagEntity(RepositoryMutePattern.GET_ALL);

        if (queryResult.IsSuccess)
            return Ok(queryResult.EntityCollection);
        else return BadRequest($"Could not observe existing Place objects.\n {queryResult.Message}");
    }

    [HttpGet]
    public async Task<IActionResult> GetSinglePlace([FromBody] long placeId)
    {
        var requestParameters = new RequestQueryParameters(EntityId: placeId);
        var queryResult = await _repositoryProxy.QueryPlaceTagEntity(RepositoryMutePattern.GET_SINGLE, requestParameters);

        if (queryResult.IsSuccess)
            return Ok(queryResult.Entity);
        else return BadRequest($"Could not observe existing Place object.\n {queryResult.Message}");
    }

    [HttpGet]
    public async Task<IActionResult> GetMultiplePlaces([FromBody] IEnumerable<long> placesId)
    {
        var requestParameters = new RequestQueryParameters(EntitiesId: placesId);
        var queryResult = await _repositoryProxy.QueryPlaceTagEntity(RepositoryMutePattern.GET_MULTIPLE_BY_ID, requestParameters);

        if (queryResult.IsSuccess)
            return Ok(queryResult.EntityCollection);
        else return BadRequest($"Could not observe existing Place objects.\n {queryResult.Message}");
    }

    [HttpGet]
    public async Task<IActionResult> GetMultiplePlaces([FromBody] RequestParameters placeRequestParameters)
    {
        var requestParameters = new RequestQueryParameters(RequestParameters: placeRequestParameters);
        var queryResult = await _repositoryProxy.QueryPlaceTagEntity(RepositoryMutePattern.GET_MULTIPLE_BY_CONDITION, requestParameters);

        if (queryResult.IsSuccess)
            return Ok(queryResult.EntityCollection);
        else return BadRequest($"Could not observe existing Place objects.\n {queryResult.Message}");
    }
}