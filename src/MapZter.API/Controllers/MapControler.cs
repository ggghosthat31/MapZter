using MapZter.API.Services.Entity;
using MapZter.Entity.Models;
using MapZter.Entity.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace;

[Route("api/[controller]")]
[ApiController]
public class MapControler : ControllerBase
{
    private readonly MapClientService _mapClientService;

    public MapControler(MapClientService mapClientService)
    {
        _mapClientService = mapClientService;
    }

    [HttpGet]
    public async Task<IActionResult> GetMap()
    {
        var mapSource = await _mapClientService.GetMap();
        return Content(mapSource, "text/html");
    }

    [HttpGet]
    public async Task<IActionResult> GetServerStatus()
    {
        var mapSource = await _mapClientService.GetServerStatus();
        return Content(mapSource, "text/plain");
    }

    [HttpGet]
    public async Task<IActionResult> Geocode([FromBody]string address)
    {
        var geocodeResult = await _mapClientService.Geocode(address);
        return Ok(geocodeResult);
    }

    [HttpGet]
    public async Task<IActionResult> ReverseGeocode([FromBody]GeoPoint geoPoint)
    {
        var reverseGeocodeResult = await _mapClientService.ReverseGeocode(geoPoint);
        return Ok(reverseGeocodeResult);
    }

    [HttpGet]
    public async Task<IActionResult> ManualReverseGeocode([FromBody]GeoPointDto geoPoint)
    {
        var reverseGeocodeResult = await _mapClientService.ReverseGeocode(geoPoint.Latitude, geoPoint.Longitude);
        return Ok(reverseGeocodeResult);
    }

    [HttpGet]
    public async Task<IActionResult> Lookup([FromBody]LookupDto lookupDto)
    {
        var reverseGeocodeResult = await _mapClientService.Lookup(lookupDto.Type, lookupDto.PlaceId);
        return Ok(reverseGeocodeResult);
    }
}