using MapZter.API.Services.Entity;
using MapZter.Entity.Models;
using MapZter.Entity.Dto;
using Microsoft.AspNetCore.Mvc;
using MapZter.Repository;
using MapZter.Proxy;
using MapZter.Proxy.Interactions;
using MapZter.Entities.RequestFeatures;

namespace MyApp.Namespace;

[Route("api/[controller]")]
[ApiController]
public class PlaceControler : ControllerBase
{
    private readonly RepositoryProxy _repositoryProxy;

    public PlaceControler(RepositoryProxy repositoryProxy)
    {
        _repositoryProxy = repositoryProxy;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePlace([FromBody] Place place)
    {
        var commandResult = await _repositoryProxy.CommandAsync(RepositoryMutePattern.CREATE, place);

        if (commandResult.IsSuccess)
            return Ok();            
        else return BadRequest($"Could not create a new Place.\n {commandResult.Message}");
    }

    [HttpPut]
    public async Task<IActionResult> UpdatePlace([FromBody] Place place)
    {
        var commandResult = await _repositoryProxy.CommandAsync(RepositoryMutePattern.UPDATE, place);

        if (commandResult.IsSuccess)
            return Ok();            
        else return BadRequest($"Could not update Place.\n {commandResult.Message}");
    }

    [HttpDelete]
    public async Task<IActionResult> DeletePlace([FromBody] Place place)
    {
        var commandResult = await _repositoryProxy.CommandAsync(RepositoryMutePattern.DELETE, place);

        if (commandResult.IsSuccess)
            return Ok();            
        else return BadRequest($"Could not delete Place.\n {commandResult.Message}");
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPlaces()
    {
        var queryResult = await _repositoryProxy.QueryPlaceEntity(RepositoryMutePattern.GET_ALL);

        if (queryResult.IsSuccess)
            return Ok(queryResult.EntityCollection);
        else return BadRequest($"Could not observe existing Place objects.\n {queryResult.Message}");
    }

    [HttpGet]
    public async Task<IActionResult> GetSinglePlace([FromBody] long placeId)
    {
        var requestParameters = new RequestQueryParameters(EntityId: placeId);
        var queryResult = await _repositoryProxy.QueryPlaceEntity(RepositoryMutePattern.GET_SINGLE, requestParameters);

        if (queryResult.IsSuccess)
            return Ok(queryResult.Entity);
        else return BadRequest($"Could not observe existing Place object.\n {queryResult.Message}");
    }

    [HttpGet]
    public async Task<IActionResult> GetMultiplePlaces([FromBody] IEnumerable<long> placesId)
    {
        var requestParameters = new RequestQueryParameters(EntitiesId: placesId);
        var queryResult = await _repositoryProxy.QueryPlaceEntity(RepositoryMutePattern.GET_MULTIPLE_BY_ID, requestParameters);

        if (queryResult.IsSuccess)
            return Ok(queryResult.EntityCollection);
        else return BadRequest($"Could not observe existing Place objects.\n {queryResult.Message}");
    }

    [HttpGet]
    public async Task<IActionResult> GetMultiplePlaces([FromBody] RequestParameters placeRequestParameters)
    {
        var requestParameters = new RequestQueryParameters(RequestParameters: placeRequestParameters);
        var queryResult = await _repositoryProxy.QueryPlaceEntity(RepositoryMutePattern.GET_MULTIPLE_BY_CONDITION, requestParameters);

        if (queryResult.IsSuccess)
            return Ok(queryResult.EntityCollection);
        else return BadRequest($"Could not observe existing Place objects.\n {queryResult.Message}");
    }
}