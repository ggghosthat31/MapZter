using MapZter.Entity.Models;

namespace MapZter.Contracts.Interfaces.Services;

public interface IMapService : IDisposable
{
    public Task<string> GetMap();

    public Task<string> GetServerStatus();

    public Task<Place[]> Geocode(string address);

    public Task<Place> ReverseGeocode(double lat, double lon);

    public Task<Place> ReverseGeocode(GeoPoint geoPoint);

    public Task<Place[]> Lookup(char type, long placeId);
}