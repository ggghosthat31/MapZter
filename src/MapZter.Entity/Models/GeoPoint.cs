namespace MapZter.Entity.Models;

public struct GeoPoint(double lat, double lon) : IEntity, IPointMatchable<GeoPoint>
{
    public long Id { get; set; }

    public double Latitude { get; set; } = lat;

    public double Longitude { get; set; } = lon;

    private static double SliceVector(double value) => 
        Math.Round(value, 2, MidpointRounding.ToZero);

    public bool Equals(GeoPoint obj)
    {
        if (obj is GeoPoint geoPoint)
        {
            var eqInputLat = SliceVector(geoPoint.Latitude);
            var eqInputLon = SliceVector(geoPoint.Longitude);

            var eqSelfLat = SliceVector(Latitude);
            var eqSelfLon = SliceVector(Longitude);

            return eqSelfLat == eqInputLat && eqSelfLon == eqInputLon;
        }

        return false;
    }

    public bool Match(GeoPoint obj) =>
        Id == obj.Id;

    public override string ToString() => 
        $"{Latitude} {Longitude}";
}