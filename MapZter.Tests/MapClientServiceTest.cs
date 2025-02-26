using MapZter.API.Services;

namespace MapZter.Tests;

public class MapClientServiceTest
{
    private readonly MapClientService _mapService;

    public MapClientServiceTest()
    {
        var http = GenerateHttpClient();
        _mapService = new(http);
    }


//50.6405323,5.5675366,18.71z

    private HttpClient GenerateHttpClient()
    {
        return new HttpClient();
    }


    [Fact]
    public void ReverseGeocodeFetchTest()
    {
        var lat = 50.6405323d;
        var lon = 5.5675366d;
        var resp = _mapService.ReverseGeocode(lat, lon).Result;
        System.Console.WriteLine("Reverse geocode test case: ");
        System.Console.WriteLine(resp);
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
