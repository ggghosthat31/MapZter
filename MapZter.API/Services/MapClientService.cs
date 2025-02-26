namespace MapZter.API.Services;

public sealed class MapClientService
{
    private readonly HttpClient _httpClient;

    public MapClientService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> ReverseGeocode(double lat, double lon)
    {
        string url = $"https://nominatim.openstreetmap.org/reverse?format=json&lat={lat}&lon={lon}";
        _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("geo");
        var response = await _httpClient.GetAsync(url);
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<object> Geocode(string address)
    {
        string url = $"https://nominatim.openstreetmap.org/search?format=json&q={address}&limit=1";
        _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("geo");
        var response = await _httpClient.GetAsync(url);
        return await response.Content.ReadAsStringAsync();
    }
}