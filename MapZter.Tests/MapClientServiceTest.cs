using MapZter.API.Services;
using MapZter.Entity.Models;

namespace MapZter.Tests;

public class MapClientServiceTest
{
    private readonly MapClientService _mapService;

    public MapClientServiceTest()
    {
        var httpClient = GenerateHttpClient();
        _mapService = new(httpClient);
    }

    private HttpClient GenerateHttpClient()
    {
        var client =  new HttpClient();
        client.DefaultRequestHeaders.UserAgent.ParseAdd("test");
        return client;
    }

    [Fact]
    public void ReverseGeocodeFetchPlaceArrayTest()
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
    public void ReverseGeocodeRetrievedResultTest()
    {
        var lat = 50.6405323d;
        var lon = 5.5675366d;
        var actual = new GeoPoint(lat, lon);
        var resp = _mapService.ReverseGeocode(lat, lon).Result;
        var retrieved = resp.GetGeoPoint();
        Assert.Equal(retrieved, actual);
    }

    [Fact]
    public void GeocodeRetrievedResultTest()
    {        
        string address = "Bd de la Sauvenière 146";
        var resp = _mapService.Geocode(address).Result;

        Assert.NotNull(resp);
        Assert.NotEmpty(resp);
    }
}