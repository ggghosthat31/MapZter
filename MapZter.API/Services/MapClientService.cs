using MapZter.Contracts.Interfaces;
using MapZter.Entity.Models;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Text.Encodings;

namespace MapZter.API.Services;


public record MapServiceConfiguration();

public sealed class MapClientService
{
    private readonly string _mapFilePath;
    private readonly HttpClient _httpClient;
    private readonly ILoggerManager _loggerManager;

    public MapClientService(
        string mapFilePath,
        HttpClient httpClient,
        ILoggerManager loggerManager)
    {
        _mapFilePath = mapFilePath;
        _httpClient = httpClient;
        _loggerManager = loggerManager;
    }

    public async Task<string> GetMap()
    {
        string content = String.Empty;
        try
        {
            using (FileStream fileStream = new FileStream(_mapFilePath, FileMode.Open, FileAccess.Read))
            using (StreamReader reader = new StreamReader(fileStream, Encoding.UTF8))
                content = reader.ReadToEnd();
        }
        catch (Exception ex)
        {
            _loggerManager.LogError(ex.Message);
        }

        return content;
    }

    public async Task<Place> ReverseGeocode(double lat, double lon)
    {
        string url = $"https://nominatim.openstreetmap.org/reverse?format=json&lat={lat}&lon={lon}";
        var response = await _httpClient.GetAsync(url);
        var data =  await response.Content.ReadAsStringAsync();
        var parsed = JsonConvert.DeserializeObject<Place>(data);
        return parsed;
    }

    public async Task<Place> ReverseGeocode(GeoPoint geoPoint)
    {
        string url = $"https://nominatim.openstreetmap.org/reverse?format=json&lat={geoPoint.Latitude}&lon={geoPoint.Longitude}";
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