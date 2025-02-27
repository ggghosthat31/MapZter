namespace MapZter.Entity.Models;

public struct GeoPoint(double lat, double lon)
{
    public double Latitude { get; set; } = lat;

    public double Longitude { get; set; } = lon;
}