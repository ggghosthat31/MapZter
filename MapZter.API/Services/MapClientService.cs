using MapZter.Entity.Models;
using Newtonsoft.Json;

namespace MapZter.API.Services;

public sealed class MapClientService
{
    private readonly HttpClient _httpClient;

    public MapClientService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Place> ReverseGeocode(double lat, double lon)
    {
        string url = $"https://nominatim.openstreetmap.org/reverse?format=json&lat={lat}&lon={lon}";
        var response = await _httpClient.GetAsync(url);
        var data =  await response.Content.ReadAsStringAsync();
        var parsed = JsonConvert.DeserializeObject<Place>(data);
        return parsed;
    }

    public async Task<Place[]> Geocode(string address)
    {
        string url = $"https://nominatim.openstreetmap.org/search?format=json&q={address}";
        var response = await _httpClient.GetAsync(url);
        var data =  await response.Content.ReadAsStringAsync();
        var parsed = JsonConvert.DeserializeObject<Place[]>(data);
        return parsed;
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }
}