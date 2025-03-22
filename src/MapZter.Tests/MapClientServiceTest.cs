using MapZter.API.Services.Entity;
using MapZter.Entity.Models;
using MapZter.Logger;

namespace MapZter.Tests;

public class MapClientServiceTest
{
    private readonly MapClientService _mapService;

    public MapClientServiceTest()
    {
        var httpClient = GenerateHttpClient();
        string mapFilePath = $"{Environment.CurrentDirectory}/md.html";
        var loggerManager = new LoggerManager();
        _mapService = new(mapFilePath, httpClient, loggerManager);
    }

    private HttpClient GenerateHttpClient()
    {
        var client =  new HttpClient();
        client.DefaultRequestHeaders.UserAgent.ParseAdd("test");
        return client;
    }

    [Fact]
    public void ReverseGeocodeFetchPlaceArray()
    {
        var lat = 50.6405323d;
        var lon = 5.5675366d;
        var resp = _mapService.ReverseGeocode(lat, lon).Result;
        Assert.IsType<Place>(resp);
    }

    [Fact]
    public void GeocodeFetchTest()
    {
        string address = "Bd de la Sauvenière 146";
        var resp = _mapService.Geocode(address).Result;
        Assert.IsType<Place[]>(resp);
    }

    [Fact]
    public void ReverseGeocodeGeoPointParamFetchPlaceTest()
    {
        var lat = 50.6405323d;
        var lon = 5.5675366d;
        var geoPoint = new GeoPoint(lat, lon);
        var resp = _mapService.ReverseGeocode(geoPoint).Result;
        var retrieved = resp.GetGeoPoint();
        Assert.IsType<Place>(resp);
        Assert.True(retrieved.Equals(geoPoint));
    }

    [Fact]
    public void ReverseGeocodeRetrievedResultTest()
    {
        var lat = 50.6405323d;
        var lon = 5.5675366d;
        var actual = new GeoPoint(lat, lon);
        var resp = _mapService.ReverseGeocode(lat, lon).Result;
        var retrieved = resp.GetGeoPoint();
        Assert.True(retrieved.Equals(actual));
    }

    [Fact]
    public void GeocodeRetrievedResultTest()
    {        
        string address = "Bd de la Sauvenière 146";
        var resp = _mapService.Geocode(address).Result;

        Assert.NotNull(resp);
        Assert.NotEmpty(resp);
    }

    [Fact]
    public void RetriveMap()
    {
        var resp = _mapService.GetMap().Result;
        Assert.NotNull(resp);
    }
}