namespace MapZter.API.Services;

public sealed class MapClientService
{
    private readonly HttpClient _httpClient;

    public MapClientService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<object> ReverseGeocode(double lat, double lon)
    {
        string url = $"https://nominatim.openstreetmap.org/reverse?format=json&lat={lat}&lon={lon}&zoom=18&addressdetails=1";
        var response = await _httpClient.GetStringAsync(url);
        return null;
    }

    public async Task<object> Geocode(string address)
    {
        string url = $"https://nominatim.openstreetmap.org/search?format=json&q={address}&limit=1";
        var response = await _httpClient.GetStringAsync(url);
        return response;
    }
}