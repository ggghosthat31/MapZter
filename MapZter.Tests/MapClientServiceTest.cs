using MapZter.API.Services;

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
    public void ReverseGeocodeFetchTest()
    {
        var lat = 50.6405323d;
        var lon = 5.5675366d;
        var resp = _mapService.ReverseGeocode(lat, lon).Result;
        System.Console.WriteLine("Reverse geocode test case: ");
        System.Console.WriteLine($"{resp.Latitude} {resp.Longitude}");
    }

    [Fact]
    public void GeocodeFetchTest()
    {
        string address = "Bd de la Sauvenière 146";
        var resp = _mapService.Geocode(address).Result;
        System.Console.WriteLine("Geocode test case: ");
        System.Console.WriteLine(resp);
    }
}
